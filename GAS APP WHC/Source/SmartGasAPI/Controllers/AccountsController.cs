using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartGasAPI.Data;
using SmartGasAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartGasAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountsController : BaseApiController<AccountsController>
    {
        private SmartGas_MRO_DBcontext _mroDBContext;
        private SmartGas_SP_DBContext _spDBContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;

        public AccountsController(SmartGas_MRO_DBcontext mroContext, SmartGas_SP_DBContext spContext, IConfiguration configuration)
        {
            _mroDBContext = mroContext;
            _spDBContext = spContext;

            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var us = HttpContext.User;
            return Ok(_spDBContext.Users);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] pUser user)
        {
            var context = InstanceDB.context(user.Department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;

            object userDB;
            bool passSuccess = false;
            string userName = "";
            if (context != null)
            {
                userDB = _mroDBContext.Users.FirstOrDefault(u => u.USER_ID.ToLower() == user.Id.ToLower());
                userName = ((Models.MRO.User)userDB).USER_NAME;
                if (((Models.MRO.User)userDB).PASSWORD == user.Password)
                {
                    passSuccess = true;
                }
            }
            else
            {
                userDB = _spDBContext.Users.FirstOrDefault(u => u.USER_ID.ToLower() == user.Id.ToLower());
                userName = ((Models.SPAREPART.User)userDB).USER_NAME;
                if (((Models.SPAREPART.User)userDB).PASSWORD == user.Password)
                {
                    passSuccess = true;
                }
            }

            if (userDB == null) return StatusCode(StatusCodes.Status404NotFound);

            if (!passSuccess) return Unauthorized();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(ClaimTypes.Name, user.Id)
            };

            var token = _auth.GenerateAccessToken(claims);

            _log4net.Info("Login success : " + userName);

            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_Id = user.Id,
                user_Name = userName
            });
        }
    }
}

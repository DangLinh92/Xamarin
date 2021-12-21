using Microsoft.AspNetCore.Mvc;
using SmartGasAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private SmartGas_MRO_DBcontext _mroDBContext;
        private SmartGas_SP_DBContext _spDBContext;

        public AccountsController(SmartGas_MRO_DBcontext mroContext, SmartGas_SP_DBContext spContext)
        {
            _mroDBContext = mroContext;
            _spDBContext = spContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(_spDBContext.Users);
        }
    }
}

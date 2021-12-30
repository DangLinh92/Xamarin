using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Controllers
{
    public class BaseApiController<T> : Controller
    {
       public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(T));

        public IActionResult Index()
        {
            return View();
        }
    }
}

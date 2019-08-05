using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThothCore.Controllers
{
    public class LoginController : Controller
    {
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
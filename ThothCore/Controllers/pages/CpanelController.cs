using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThothCore.Controllers.pages
{
	[Authorize]
    public class CpanelController : Controller
    {
		[Route("cpanel/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
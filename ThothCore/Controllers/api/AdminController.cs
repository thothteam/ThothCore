using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThothCore.Domain.Models;
using ThothCore.Domain.Persistence.Contexts;

namespace ThothCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        ApplicationDbContext _context;
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("getusers")]

        public IActionResult Users()
        {
            var users = _context.Users.ToList();

            return new JsonResult(new{users});
        }

        [Route("getroles")]

        public IActionResult Roles()
        {
            var roles = _context.Roles.ToList();

            return new JsonResult(new{roles});
        }
    }
}
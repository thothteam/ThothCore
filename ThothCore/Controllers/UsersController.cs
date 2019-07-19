using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThothCore.Domain.Models;
using ThothCore.Domain.ViewModels;

namespace ThothCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userManager.Users.ToList();
            return new JsonResult(new{users});
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = _userManager.FindByIdAsync(id);
            return new JsonResult(new { user });
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Email = model.Email, UserName = model.Email, Name = model.Name };

                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return BadRequest(result.Errors);
            }

            return BadRequest();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}

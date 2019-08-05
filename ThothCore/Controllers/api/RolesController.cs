using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ThothCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RolesController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;

        // GET: api/Roles
        [HttpGet]
        public IActionResult Get()
        {
            var roles = _roleManager.Roles.ToList();
            return new JsonResult(new{roles});
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var role = _roleManager.FindByIdAsync(id);
            return new JsonResult(new { role });
        }

        // POST: api/Roles
        [HttpPost]
        public void Post([FromBody] string value)
        {
          IdentityRole role = new IdentityRole(value);
          var result =_roleManager.CreateAsync(role);
        }

        // PUT: api/Roles/5
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

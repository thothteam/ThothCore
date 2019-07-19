using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ThothCore.Domain.Models;

namespace ThothCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;

        public AuthenticationController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post(AuthenticationRequest authRequest, [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            //Проверка данных пользователя 

            //var identity = GetIdentity(authRequest.Email, authRequest.Password);
            var user = await _userManager.FindByEmailAsync(authRequest.Email);
            if (user == null)
            {
                Response.StatusCode = 400;
                Response.WriteAsync("Invalid email");
                return BadRequest();
            }

            var passwordOk = await _userManager.CheckPasswordAsync(user, authRequest.Password);
            if (!passwordOk)
            {
                Response.StatusCode = 400;
                Response.WriteAsync("Invalid password");
                return BadRequest();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            //Создаем утверждения для токена

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, authRequest.Email),
            };

            //Генерируем JWT

            var token = new JwtSecurityToken(
                issuer: "ThothCore",
                audience: "ThothCoreClient",
                claims: claims.Concat(userClaims).ToArray(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(signingEncodingKey.GetKey(), signingEncodingKey.SigningAlgorithm)
                );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            const string jwtShemeName = "JwtBearer";
            await _userManager.SetAuthenticationTokenAsync(user, jwtShemeName, jwtShemeName, jwtToken);
            return new JsonResult(new
            {
                access_token = jwtToken,
                username = user.Name
            });
        }


        private List<Person> people = new List<Person>
        {
            new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Person { Login="qwerty", Password="55555", Role = "user" }
        };
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
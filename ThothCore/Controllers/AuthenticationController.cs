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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using ThothCore.Domain.Models;

namespace ThothCore.Controllers
{
	[EnableCors("_allowAllOrigins")]
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
		public async Task<ActionResult> Post(AuthenticationRequest authRequest,
			[FromServices] IJwtSigningEncodingKey signingEncodingKey)
		{
			//Проверка данных пользователя 
			if (authRequest.Email == null || authRequest.Password == null)
			{
				var errorText = "Invalid email or password";
				Response.StatusCode = 400;
				return BadRequest(errorText);
			}

			var user = await _userManager.FindByEmailAsync(authRequest.Email);

			if (user == null)
			{
				var errorText = "Invalid email";
				Response.StatusCode = 400;
				return BadRequest(errorText);
			}

			var passwordOk = await _userManager.CheckPasswordAsync(user, authRequest.Password);
			if (!passwordOk)
			{
				var errorText = "Invalid password";
				Response.StatusCode = 400;
				return BadRequest(errorText);
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


			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			Response.Cookies.Append(
				"accessToken",
				tokenString,
				new CookieOptions()
				{
					Path = "/"
				}
			);

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
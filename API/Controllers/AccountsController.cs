using API.Data.Entities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IJWTTokenGenerator _jwtToken;

		public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, IJWTTokenGenerator jwtToken)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtToken = jwtToken;
		}

		// POST api/account/login
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginModel model)
		{
			var userFromDb = await _userManager.FindByNameAsync(model.Username);
			if (userFromDb == null)
			{
				return BadRequest(new
				{
					message = "Unable to login",
					status = 400
				});
			}
			var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, model.Password, false);

			if (!result.Succeeded)
			{
				return BadRequest(new
				{
					message = "Unable to login",
					status = 400
				});
			}
			return Ok(new
			{
				token = _jwtToken.GenerateToken(userFromDb),
				status = 200
			});
		}
	}
}

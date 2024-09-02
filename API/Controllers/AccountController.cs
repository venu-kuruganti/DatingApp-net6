using System;
using System.Security.Cryptography;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using API.Data;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
	public class AccountController : BaseAPIController
	{
		private readonly DataContext context;
		private readonly ITokenService tokenService;

		public AccountController(DataContext _context, ITokenService _tokenService)
		{
			context = _context;
			tokenService = _tokenService;
		}

		[HttpPost("register")] //account/register
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{

			if (await UserExists(registerDto.Username))
			{
				return BadRequest("Username already exists");
			}

			using var hmac = new HMACSHA512(); //The hashing algorithm

			var user = new AppUser
			{
				UserName = registerDto.Username.ToLower(),
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
				PasswordSalt = hmac.Key
			};

			//Store it in the database
			context.Users.Add(user);

			await context.SaveChangesAsync();

			return new UserDto
			{
				Username = user.UserName,
				Token = tokenService.CreateToken(user)
			};
			
		}

		[HttpPost("login")] //account/login
		public async Task<ActionResult<UserDto>> Login(LoginDto login)
		{
			var user = await context.Users.Where(u => u.UserName.ToLower() == login.Username.ToLower()).FirstOrDefaultAsync();

			if (user==null)
			{
				return Unauthorized("Invalid Username");
			}

			using var hmac = new HMACSHA512(user.PasswordSalt);

			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

			for (int i = 0; i < computedHash.Length; i++)
			{
				if (computedHash[i] != user.PasswordHash[i])
				{
					return Unauthorized("Invalid Password");
				}
			}

			return new UserDto
			{
				Username = user.UserName,
				Token = tokenService.CreateToken(user)
			};

		}

		private async Task<bool> UserExists(string userName)
		{
			return await context.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
		}

	}
}


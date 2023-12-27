﻿using BooksWebAPI.Data;
using BooksWebAPI.Hellper;
using BooksWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (request == null)
                return BadRequest();

            var checkUserEmail = await _context.Users.Where(user => user.Email == request.Email).FirstOrDefaultAsync();
            if (checkUserEmail != null)
                return BadRequest("This Email already in use.");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User { Email = request.Email, PasswordHash = passwordHash };

            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) { return BadRequest(ex.Message);}

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            var user = await _context.Users.Where(user => user.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
                return BadRequest("User not found.");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BadRequest("Wrong password");

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            return TokenService.CreateToken(user, _configuration);
        }
    }
}

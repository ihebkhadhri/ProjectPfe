﻿using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectPfe.Models;
using ProjectPfe.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuth.WebApi.Controllers
{
      [ApiController]
    [Route("[controller]")]

    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        UserService _context;

        public TokenController(IConfiguration config, UserService context)
        {
            _configuration = config;
            _context = context;
        }

        [Route("GetUser/{Email}/{Password}")]
        public async Task<IActionResult> Post(String Email, String Password )
        {
            if ( Email != null && Password != null)
            {
                var user =  GetUser(Email, Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("DisplayName", user.DisplayName),
                        new Claim("UserName", user.Username),
                        
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);


                    UserInfo u=new UserInfo();
                    u.UserName = Email;
                    u.jwttoken = token.ToString();
                    u.UserId = user.Id;
                    u.Role = user.UserRole.ToString();

                    UserConnected.user = user;
                    return Ok(u);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private  User GetUser(string email, string password)
        {
            return  _context.GetByEmailPassword(email, password);
        }
    }
}
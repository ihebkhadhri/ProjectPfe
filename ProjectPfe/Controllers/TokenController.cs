
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Identity;
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
        //private readonly UserManager<User> _userManager;
        

        public TokenController(IConfiguration config, UserService context)
        {
            _configuration = config;
            _context = context;
            //_userManager = userManager;
            
        }
        [HttpGet]
        [Route("GetUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            return _context.Get();
        }
        [HttpDelete(Name = "GetUser")]
        [Route("GetUser/{id}")]
        public ActionResult<User> GetUser(string id)
        {
            var user = _context.Get(id);
            return (user);
        }
        [HttpPost(Name = "Create")]
        [Route("Create")]
        public Boolean Create(User user)
        {
            if (user.UserRole == UserRole.Utilisateur)
                user.validate = false;
            else
                user.validate = true;
            _context.Create(user);
            return true;
          
        }
        [HttpDelete(Name = "DeleteUser")]
        [Route("DeleteUser/{id}")]
        public IActionResult Delete(string id)
        {
            var user = _context.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Remove(user.Id);
            return Ok($"categorie with Id = {id} deleted");
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
                        new Claim("DisplayName", user.LastName),
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


        [HttpGet(Name = "Getgroup")]
        [Route("Getgroup")]
        public List<User> GetUserByrole()
        {
           var groups= _context.Getgroup();
            return groups;
        }

        [HttpPut(Name = "validategroup")]
        [Route("validategroup/{id}")]
        public User validategroup(string id)
        {
          var User= _context.Get(id);
            User.validate = true;
            _context.Update(id, User);
            return User;
        }



    }
}
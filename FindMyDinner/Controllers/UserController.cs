using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FindMyDinner.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace FindMyDinner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettingsModel _appsettings;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IOptions<AppSettingsModel> appsettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appsettings = appsettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST : api/user/Register

        public async Task<object> PostUser(UserModel model)
        {
            var applicationUser = new ApplicationUser(){
                UserName=model.UserName,
                Email=model.Email,
                PhoneNumber=model.PhoneNumber,
                FullName=model.FullName,



            };
            try {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex){
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : api/user/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user!=null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.Jwt_Secret)),SecurityAlgorithms.HmacSha256Signature),
                };
                var tokenHnadler = new JwtSecurityTokenHandler();
                var securityToken = tokenHnadler.CreateToken(tokenDescriptor);
                var token = tokenHnadler.WriteToken(securityToken);
                return Ok(new {token});

            }

            else
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("UserProfile")]
        //Get : api/user/UserProfile
        public async Task<object> GetUserProfile()
        {
            var userId = User.Claims.First(e => e.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.UserName,
                user.Email,
                user.PhoneNumber,

            };

        }

    }
}
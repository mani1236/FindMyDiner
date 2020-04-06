using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindMyDinner.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMyDinner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

    }
}
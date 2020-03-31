using FindMyDinner.Contracts.Models;
using FindMyDinner.Contracts.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FindMyDinner.Commands.User
{
    public class PostUserCommand : IPostUserCommand
    {
        readonly UserService _userService;
       public PostUserCommand(UserService userService) {
            _userService = userService;
        }
        public async Task<IActionResult> ExecuteAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken = default)
        {
            var results = await _userService.CreateAsync(userRequestDto);
            return new OkObjectResult(results);
          
        }
    }
}

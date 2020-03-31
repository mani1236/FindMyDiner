
using FindMyDinner.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FindMyDinner.Commands.User
{
    public interface IPostUserCommand
    {
        Task<IActionResult> ExecuteAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken = default);
    }
}

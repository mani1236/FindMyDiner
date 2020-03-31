using FindMyDinner.Contracts.Models;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindMyDinner.Contracts.Services.User
{
    public interface IUserService
    {
        Task<string> CreateAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken = default);
    }
}

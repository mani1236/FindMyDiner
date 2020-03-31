using FindMyDinner.Domain.Infrastracture.Repositories;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FindMydinner.Domain.Model.Entities;
using FindMyDinner.Contracts.Models;

namespace FindMyDinner.Contracts.Services.User
{
    public class UserService : IUserService
    {
        readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> CreateAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken = default)
        {
            var entity = new FindMydinner.Domain.Model.Entities.User() {
                UserId = Guid.NewGuid(),
                UserName = userRequestDto.firstname +" " +userRequestDto.lastname,
                Email = userRequestDto.email,
                MobileNo=userRequestDto.modilenumber             
            };
            await _userRepository.CreateAsync(entity, cancellationToken);
            await _userRepository.SaveAsync(cancellationToken);
            return "ok";
        }
    }
}

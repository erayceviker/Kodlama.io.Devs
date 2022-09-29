using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<AccessToken>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IAuthService _authService;

            public RegisterUserCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IAuthService authService)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _authService = authService;
            }

            public async Task<AccessToken> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserAlreadyExists(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password,out passwordHash,out passwordSalt);

                var user = new User
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };

               var userRegistered = await _userRepository.AddAsync(user);

               var result = _authService.CreateAccessToken(userRegistered);

               return result;
            }
        }
    }
}

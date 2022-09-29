using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AccessToken>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessToken>
        {
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;


            public LoginUserCommandHandler(UserBusinessRules userBusinessRules, IUserRepository userRepository, IMapper mapper, IAuthService authService)
            {
                _userBusinessRules = userBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {

                await _userBusinessRules.UserToCheck(request.UserForLoginDto.Email);

                var user = await _userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);

                _userBusinessRules.VerifyPasswordHashWhenLogin(request.UserForLoginDto.Password,user.PasswordHash,user.PasswordSalt);

                var result = _authService.CreateAccessToken(user);

                return result;
            }
        }
    }
}

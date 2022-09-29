using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private ITokenHelper _tokenHelper;

        public AuthService(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }
        public AccessToken CreateAccessToken(User user)
        {
            var claims = _userRepository.GetClaims(user);

            var accessToken = _tokenHelper.CreateToken(user, claims);

            return accessToken;
        }
    }
}

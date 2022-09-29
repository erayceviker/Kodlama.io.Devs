using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Hashing;
using Domain.Entities;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserToCheck(string mail)
        {
            var userToCheck = await _userRepository.GetAsync(x => x.Email == mail);

            if (userToCheck == null)
            {
                throw new BusinessException("User Not Found");
            }
        }

        public void VerifyPasswordHashWhenLogin(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
            {
                throw new BusinessException("the password is incorrect");
            }
        }

        public async Task UserAlreadyExists(string mail)
        {
            var user = await _userRepository.GetAsync(x => x.Email == mail);

            if (user != null)
            {
                throw new BusinessException("User already exsists");
            }
        }

    }
}

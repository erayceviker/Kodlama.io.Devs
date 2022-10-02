using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        [FromRoute]
        public int Id { get; set; }

        [FromBody]
        public UpdateUserBodyDto updateUserBodyDto { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IMapper mapper)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserShouldExistWhenRequested(request.Id);

                User user = await _userRepository.GetAsync(x => x.Id == request.Id);

                user.FirstName = request.updateUserBodyDto.FirstName;
                user.LastName = request.updateUserBodyDto.LastName;

                await _userRepository.UpdateAsync(user);

                return Unit.Value;
            }
        }
    }
}

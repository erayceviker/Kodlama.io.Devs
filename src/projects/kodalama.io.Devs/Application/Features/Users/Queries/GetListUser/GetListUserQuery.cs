using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Models;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetListUser
{
    public class GetListUserQuery : IRequest<UserListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery,UserListModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IMapper mapper)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await _userRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                UserListModel mappedUsers = _mapper.Map<UserListModel>(users);

                return mappedUsers;
            }
        }
    }
}

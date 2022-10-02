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
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetListByDynamicUser
{
    public class GetListByDynamicUserQuery : IRequest<UserListModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public class GetListByDynamicUserQueryHandler : IRequestHandler<GetListByDynamicUserQuery,UserListModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetListByDynamicUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetListByDynamicUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await _userRepository.GetListByDynamicAsync(dynamic: request.Dynamic,
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                var mappedUsers = _mapper.Map<UserListModel>(users);

                return mappedUsers;
            }
        }
    }
}

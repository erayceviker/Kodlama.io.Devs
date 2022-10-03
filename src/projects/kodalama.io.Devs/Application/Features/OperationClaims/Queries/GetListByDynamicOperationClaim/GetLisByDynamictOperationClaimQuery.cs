using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Dynamic;

namespace Application.Features.OperationClaims.Queries.GetListByDynamicOperationClaim
{
    public class GetLisByDynamictOperationClaimQuery : IRequest<OperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic  Dynamic { get; set; }

        public class GetListOpreationClaimQueryHandler : IRequestHandler<GetLisByDynamictOperationClaimQuery, OperationClaimListModel>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public GetListOpreationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<OperationClaimListModel> Handle(GetLisByDynamictOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListByDynamicAsync
                    (dynamic:request.Dynamic,index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                var mappedOperationClaims = _mapper.Map<OperationClaimListModel>(operationClaims);

                return mappedOperationClaims;
            }
        }
    }
}

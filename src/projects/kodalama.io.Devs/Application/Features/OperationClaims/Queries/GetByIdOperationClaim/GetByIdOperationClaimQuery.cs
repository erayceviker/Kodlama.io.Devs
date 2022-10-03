using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQuery : IRequest<OperationClaimGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery,OperationClaimGetByIdDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<OperationClaimGetByIdDto> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimShouldExistWhenRequested(request.Id);

                var operationClaims = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);

                var mappedOperationClaims = _mapper.Map<OperationClaimGetByIdDto>(operationClaims);

                return mappedOperationClaims;
            }
        }
    }
}

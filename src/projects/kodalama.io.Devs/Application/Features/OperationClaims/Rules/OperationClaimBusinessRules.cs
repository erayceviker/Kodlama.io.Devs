using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimNameCanNotBeDuplicated(string name)
        {
            IPaginate<OperationClaim> result = await _operationClaimRepository.GetListAsync(x => x.Name == name, enableTracking: false);

            if (result.Items.Any())
            {
                throw new BusinessException("Operation Claim name exists.");
            }
        }

        public async Task OperationClaimShouldExistWhenRequested(int id)
        {
            var result = await _operationClaimRepository.GetAsyncAsNoTracking(x => x.Id == id);

            if (result == null)
            {
                throw new BusinessException("Operation Claim does not exist");
            }
        }
    }
}

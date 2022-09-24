using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicated(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(x => x.Name == name, enableTracking: false);

            if (result.Items.Any())
            {
                throw new BusinessException("Technology name exists.");
            }
        }

        public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
        {
            Technology result = await _technologyRepository.GetAsyncAsNoTracking(x => x.Id == id);

            if (result == null)
            {
                throw new BusinessException("Requested Technology does not exist");
            }
        }

    }
}

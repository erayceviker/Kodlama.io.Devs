using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Programming Language name exists.");
        }

        public async Task ProgrammingLanguageIsExistOrNot(int id)
        {
            ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsync(p=> p.Id == id);

            if (programmingLanguage == null) throw new BusinessException("Programming Language is not found");
        }

        public async Task ProgrammingLanguageIsExistOrNotAsNoTracking(int id)
        {
            ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsyncAsNoTracking(p => p.Id == id);

            if (programmingLanguage == null) throw new BusinessException("Programming Language is not found");
        }

    }
}

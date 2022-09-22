﻿using System;
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

        public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
        {
            ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsync(p=> p.Id == id);

            if (programmingLanguage == null) throw new BusinessException("Requested Programming Language does not exist");
        }

        public async Task ProgrammingLanguageShouldExistWhenRequestedAsNoTracking(int id)
        {
            ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsyncAsNoTracking(p => p.Id == id);

            if (programmingLanguage == null) throw new BusinessException("Requested Programming Language does not exist");
        }

    }
}

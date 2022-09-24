using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries.GetByIdTechnology
{
    public class GetByIdWithProgrammingLanguageNameTechnologyQuery : IRequest<TechnologyGetByIdWithProgrammingLanguageNameDto>
    {
        public int Id { get; set; }

        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdWithProgrammingLanguageNameTechnologyQuery, TechnologyGetByIdWithProgrammingLanguageNameDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _businessRules;

            public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules businessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<TechnologyGetByIdWithProgrammingLanguageNameDto> Handle(GetByIdWithProgrammingLanguageNameTechnologyQuery request, CancellationToken cancellationToken)
            {
                await _businessRules.TechnologyShouldExistWhenRequested(request.Id);

                Technology technology = await _technologyRepository.GetAsync(x => x.Id == request.Id,include:
                    x=>x.Include(p => p.ProgrammingLanguage));

                TechnologyGetByIdWithProgrammingLanguageNameDto mappedTechnologyGetByIdDto =  _mapper.Map<TechnologyGetByIdWithProgrammingLanguageNameDto>(technology);

                return mappedTechnologyGetByIdDto;
            }
        }
    }
}

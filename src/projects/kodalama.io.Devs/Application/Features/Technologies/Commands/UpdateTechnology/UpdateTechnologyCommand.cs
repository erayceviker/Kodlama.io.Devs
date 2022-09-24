using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly TechnologyBusinessRules _technologyBusinessRules;
            private readonly IMapper _mapper;
            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _technologyBusinessRules = technologyBusinessRules;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.Id);
                await _technologyBusinessRules.ProgrammingLanguageNameCanNotBeDuplicated(request.Name);

                Technology mappedTechnology = _mapper.Map<Technology>(request);

                Technology technology = await _technologyRepository.UpdateAsync(mappedTechnology);

                return Unit.Value;

            }
        }
    }
}

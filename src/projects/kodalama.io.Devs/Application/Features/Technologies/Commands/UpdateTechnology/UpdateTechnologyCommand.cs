using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest
    {
        [FromRoute]
        public int Id { get; set; }

        [FromBody]
        public UpdateTechnologyBodyDto Body { get; set; }

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
                await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.Id);

                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenUpdated(request.Id, request.Body.Name);

                Technology mappedUpdateTechnologyBodyDto = _mapper.Map<Technology>(request.Body);
                mappedUpdateTechnologyBodyDto.Id = request.Id;

                await _technologyRepository.UpdateAsync(mappedUpdateTechnologyBodyDto);

                return Unit.Value;

            }
        }
    }
}

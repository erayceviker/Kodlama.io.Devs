using Application.Features.Technologies.Models;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries.GetListByDynamic
{
    public class GetListByDynamicTechnologyQuery : IRequest<TechnologyGetListModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public class GetListQueryHandler : IRequestHandler<GetListByDynamicTechnologyQuery, TechnologyGetListModel>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetListQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyGetListModel> Handle(GetListByDynamicTechnologyQuery request, CancellationToken cancellationToken)
            {

                IPaginate<Technology> technologies = await _technologyRepository.GetListByDynamicAsync(dynamic:request.Dynamic, include:
                    t => t.Include(x => x.ProgrammingLanguage),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                );

                TechnologyGetListModel mappedTechnologyGetListModel = _mapper.Map<TechnologyGetListModel>(technologies);

                return mappedTechnologyGetListModel;

            }
        }
    }
}

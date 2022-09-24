using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetListByDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();

            CreateMap<Technology, TechnologyListDto>()
                .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(p => p.ProgrammingLanguage.Name))
                .ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyGetListModel>().ReverseMap();

            CreateMap<Technology, UpdateTechnologyBodyDto>().ReverseMap();

            CreateMap<Technology, TechnologyGetByIdWithProgrammingLanguageNameDto>()
                .ForMember(x=>x.ProgrammingLanguageName,opt => opt.MapFrom(p => p.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<Technology, TechnologyGetByIdDto>().ReverseMap();
        }
    }
}

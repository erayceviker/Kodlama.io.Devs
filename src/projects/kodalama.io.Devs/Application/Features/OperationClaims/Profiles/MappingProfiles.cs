using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetListByDynamicOperationClaim;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();

            CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();

            CreateMap<OperationClaimListModel, IPaginate<OperationClaim>>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();

            CreateMap<OperationClaim, OperationClaimGetByIdDto>().ReverseMap();

        }
    }
}

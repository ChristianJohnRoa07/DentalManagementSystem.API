using AutoMapper;
using DentalManagementSystem.Application.DTO.Procedures;
using DentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Procedure, ProcedureDto>();

            CreateMap<CreateProcedureDto, Procedure>().ReverseMap();
        }
    }
}

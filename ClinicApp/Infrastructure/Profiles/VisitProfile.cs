using AutoMapper;
using ClinicApp.Models;
using ClinicApp.Models.Dto;

namespace ClinicApp.Infrastructure.Profiles
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<VisitDto, Visit>();
            CreateMap<Visit, VisitDto>();
        }
    }
}

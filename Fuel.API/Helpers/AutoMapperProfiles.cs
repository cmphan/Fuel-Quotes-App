using System.Linq;
using AutoMapper;
using Fuel.API.Dtos;
using Fuel.API.Models;

namespace Fuel.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom(src => 
                src.ClientProfile.FirstOrDefault(p => p.HasPhoto == true).PhotoURL));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom(src => 
                src.ClientProfile.FirstOrDefault(p => p.HasPhoto == true).PhotoURL));
            CreateMap<ClientProfile, ClientProfileForDetailedDto>();
        }
    }
}
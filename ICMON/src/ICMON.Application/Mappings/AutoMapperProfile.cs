using AutoMapper;
using ICMON.Application.DTOs.Auth;
using ICMON.Domain.Entities;

namespace ICMON.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.Permissions, opt => opt.Ignore());
    }
}

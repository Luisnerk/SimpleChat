
using API.Dto;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppUserRegisterDto, AppUser>();
        CreateMap<AppUser, AppUserDto>()
                                    .ForMember(dest => dest.Age,
                                                opt => opt.MapFrom(x => x.BirthDate.CalculateAge()));
        CreateMap<ChatParticipant, ChatParticipantDto>().ReverseMap();
    }
}
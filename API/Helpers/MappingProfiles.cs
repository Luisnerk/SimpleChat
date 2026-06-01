
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
        CreateMap<Chat, ChatListDto>();
        CreateMap<MessageCreateDto, Message>();
        CreateMap<Message, MessageListDto>()
                                    .ForMember(dest => dest.MessageId,
                                                opt => opt.MapFrom(x => x.Id))
                                    .ForMember(dest => dest.SenderName,
                                                opt => opt.MapFrom(src => src.ChatParticipant.AppUser.Name));
    }
}
using AutoMapper;
using FlashTalk.Domain.Entities;
using FlashTalk.Service.DTOs.Messages;
using FlashTalk.Service.DTOs.Users;

namespace FlashTalk.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User

        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();
        CreateMap<UserCreationDto, User>().ReverseMap();

        //Message

        //Message
        CreateMap<Message, MessageResultDto>().ReverseMap();
        CreateMap<MessageUpdateDto, Message>().ReverseMap();
        CreateMap<MessageCreationDto, Message>().ReverseMap();
    }
}

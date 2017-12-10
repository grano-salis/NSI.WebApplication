using AutoMapper;
using IkarusEntities;
using NSI.DC.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Conversation, ConversationGetDTO>()
                .ReverseMap();
            CreateMap<Message, MessageGetDTO>()
                .ForMember(msgDto => msgDto.Message, msg => msg.MapFrom(src => src.Message1))
                .ReverseMap();
            CreateMap<Participant, ParticipantGetDTO>()
                .ReverseMap();
        }
    }
}

using AutoMapper;
using TelephoneConversations.API.DTOs;
using TelephoneConversations.Core.Models;

namespace TelephoneConversations.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Subscriber, SubscriberDTO>();
            CreateMap<SubscriberDTO, Subscriber>();

            CreateMap<Subscriber, SubscriberCreateDTO>().ReverseMap();
        }
    }
}

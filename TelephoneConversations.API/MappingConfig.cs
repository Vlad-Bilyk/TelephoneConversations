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
            CreateMap<Subscriber, SubscriberUpdateDTO>().ReverseMap();

            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<City, CityCreateDTO>().ReverseMap();

            CreateMap<Tariff, TariffDTO>().ReverseMap();
            CreateMap<Tariff, TariffCreateDTO>().ReverseMap();

            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscountCreateDTO>().ReverseMap();
        }
    }
}

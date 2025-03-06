using AutoMapper;
using TelephoneConversations.Core.Models.DTOs;
using TelephoneConversations.Core.Models.Entities;

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

            CreateMap<Call, CallDTO>()
                .ForMember(dest => dest.SubscriberName,
                opt => opt.MapFrom(src => src.Subscriber.CompanyName))
                .ForMember(dest => dest.CityName,
                opt => opt.MapFrom(src => src.City.CityName));
            CreateMap<CallDTO, Call>();
            CreateMap<Call, CallCreateDTO>().ReverseMap();
        }
    }
}

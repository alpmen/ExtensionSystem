using AutoMapper;
using Domain.Entities;
using Services.Services.ConsumerSerivces.Dtos.ResultDtos;

namespace Services.Services.Mappings
{
    public class ConsumerProfile : Profile
    {
        public ConsumerProfile()
        {
            CreateMap<Consumer, ConsumerListAllResult>();
            CreateMap<Consumer, ConsumerGetByIdResult>();
        }
    }
}

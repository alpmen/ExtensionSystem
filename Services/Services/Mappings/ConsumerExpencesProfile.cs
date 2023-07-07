using AutoMapper;
using Domain.Entities;
using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;

namespace Services.Services.Mappings
{
    public class ConsumerExpencesProfile : Profile
    {
        public ConsumerExpencesProfile()
        {
            CreateMap<ConsumerExpense, ConsumerExpenseListAllResult>();
            CreateMap<ConsumerExpense, ConsumerExpenseGetByIdResult>();
        }
    }
}

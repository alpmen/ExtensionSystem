using AutoMapper;
using Domain.Entities;
using Services.Services.ExpenseServices.Dtos.ResultDtos;

namespace Services.Services.Mappings
{
    public class ExpenceProfile : Profile
    {
        public ExpenceProfile()
        {
            CreateMap<Expense, ExpenseListAllResult>();
            CreateMap<Expense, ExpenseGetByIdResult>();
        }
    }
}
using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<ExpenseRequest, Expense>();
        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Password, 
                opt => opt.Ignore());
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, RegisterExpensesResponse>();
        CreateMap<Expense, ShortExpenseResponse>();
        CreateMap<Expense, ExpenseResponse>();
    }
}
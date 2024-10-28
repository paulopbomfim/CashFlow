using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expenses;

public class GetAllExpenseUseCase(IExpensesReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    : IGetAllExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ExpensesResponse> Execute()
    {
        var loggedUser = await _loggedUser.Get();        
        
        var result = await _repository.GetAll(loggedUser);

        return new ExpensesResponse()
        {
            Expenses = _mapper.Map<List<ShortExpenseResponse>>(result)
        };
    }
}
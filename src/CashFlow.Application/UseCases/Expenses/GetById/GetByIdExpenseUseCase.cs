using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses;

public class GetByIdExpenseUseCase(IExpensesReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    : IGetByIdExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ExpenseResponse> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        
        var response = await _repository.GetById(loggedUser, id) 
            ?? throw new NotFoundException(ErrorMessagesResource.EXPENSE_NOT_FOUND);
        
        return _mapper.Map<ExpenseResponse>(response);
    }
}
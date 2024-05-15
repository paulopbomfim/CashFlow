using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses;

public class GetByIdExpenseUseCase : IGetByIdExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdExpenseUseCase(IExpensesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ExpenseResponse> Execute(long id)
    {
        var response = await _repository.GetById(id) 
            ?? throw new NotFoundException(ErrorMessagesResource.EXPENSE_NOT_FOUND);
        
        return _mapper.Map<ExpenseResponse>(response);
    }
}
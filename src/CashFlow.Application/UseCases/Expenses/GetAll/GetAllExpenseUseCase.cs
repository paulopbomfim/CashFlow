using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UseCases.Expenses;

public class GetAllExpenseUseCase : IGetAllExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IMapper _mapper;

    public GetAllExpenseUseCase(IExpensesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ExpensesResponse> Execute()
    {
        var result = await _repository.GetAll();

        return new ExpensesResponse()
        {
            Expenses = _mapper.Map<List<ShortExpenseResponse>>(result)
        };
    }
}
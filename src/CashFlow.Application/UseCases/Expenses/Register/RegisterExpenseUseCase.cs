using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    
    public RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork uow, IMapper mapper)
    {
        _repository = repository;
        _uow = uow;
        _mapper = mapper;
    }
    
    public async Task<RegisterExpensesResponse> Execute(RegisterExpenseRequest request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);
        
        await _repository.Add(entity);

        await _uow.Commit();
        
        return _mapper.Map<RegisterExpensesResponse>(entity);
    }

    private void Validate(RegisterExpenseRequest request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}

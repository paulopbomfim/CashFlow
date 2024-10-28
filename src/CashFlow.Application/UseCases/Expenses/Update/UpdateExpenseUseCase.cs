using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses;

public class UpdateExpenseUseCase(
    IExpensesUpdateOnlyRepository repository,
    IMapper mapper,
    IUnitOfWork uow,
    ILoggedUser loggedUser)
    : IUpdateExpenseUseCase
{
    private readonly IExpensesUpdateOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _uow = uow;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task Execute(long id, ExpenseRequest request)
    {
        Validate(request);
        
        var loggedUser = await _loggedUser.Get();
        
        var expense = await _repository.GetById(loggedUser, id);

        if (expense is null)
            throw new NotFoundException(ErrorMessagesResource.EXPENSE_NOT_FOUND);

        _mapper.Map(request, expense);
        
        _repository.Update(expense);
        await _uow.Commit();
    }

    private void Validate(ExpenseRequest request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}
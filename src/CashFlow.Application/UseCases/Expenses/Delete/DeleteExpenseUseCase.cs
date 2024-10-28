using CashFlow.Domain.Repositories;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses;

public class DeleteExpenseUseCase(IExpensesWriteOnlyRepository writeOnlyRepository, IExpensesReadOnlyRepository readOnlyRepository, IUnitOfWork uow, ILoggedUser loggedUser) : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly IExpensesReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IUnitOfWork _uow = uow;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        
        var expense = await _readOnlyRepository.GetById(loggedUser, id);
        if (expense is null)
            throw new NotFoundException(ErrorMessagesResource.EXPENSE_NOT_FOUND);
        
        
        await _writeOnlyRepository.Delete(id);
        
        await _uow.Commit();
    }
}
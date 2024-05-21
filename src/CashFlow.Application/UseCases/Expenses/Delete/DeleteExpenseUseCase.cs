using CashFlow.Domain.Repositories;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _uow;

    public DeleteExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }
    
    public async Task Execute(long id)
    {
        var isDeleted = await _repository.Delete(id);
        
        if (isDeleted)
            throw new NotFoundException(ErrorMessagesResource.EXPENSE_NOT_FOUND);
        
        await _uow.Commit();
    }
}
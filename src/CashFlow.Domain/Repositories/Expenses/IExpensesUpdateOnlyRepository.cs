using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories;

public interface IExpensesUpdateOnlyRepository
{
    Task<Expense?> GetById(User user, long id);
    void Update(Expense expense);
}
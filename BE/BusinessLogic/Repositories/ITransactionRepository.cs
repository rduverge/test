
//Used to difference CuentasPorCobrar.Transaction from the
//reserved word Transaction

namespace CuentasPorCobrar.Shared;

public interface ITransactionRepository
{
    Task<Transaction?> CreateAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> RetrieveAllAsync();
    Task<Transaction?> RetrieveByIdAsync(int id);
    Task<Transaction?> UpdateAsync(int id, Transaction transaction);
    Task<bool?> DeleteAsync(int id);
    
}

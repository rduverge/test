
namespace CuentasPorCobrar.Shared;

public interface IAccountingEntryRepository
{
 
    Task<AccountingEntry?> CreateAsync(AccountingEntry accountingEntry);
    Task<IEnumerable<AccountingEntry>> RetrieveAllAsync();
    Task<AccountingEntry?> RetrieveAsync(int id);
    Task<AccountingEntry?> UpdateAsync(int id, AccountingEntry accountingEntry);
   
    Task<bool?> DeleteAsync(int id); 

}



namespace CuentasPorCobrar.Shared;

public interface ICustomerRepository
{
    Task<Customer?> CreateAsync(Customer customer);
    Task<IEnumerable<Customer>> RetrieveAllAsync();
    Task<Customer?> RetrieveByIdAsync(int id);
    Task<Customer?> UpdateAsync(int id, Customer customer);
    Task<bool?> DeleteAsync(int id);
}
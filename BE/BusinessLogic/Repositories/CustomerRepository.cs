using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;

namespace CuentasPorCobrar.Shared;

public class CustomerRepository : ICustomerRepository
{
    private static ConcurrentDictionary<int, Customer>? customerCache;

    private CuentasporcobrardbContext context;

    public CustomerRepository(CuentasporcobrardbContext context)
    {
        this.context = context;

        if(customerCache is null)
        {
            customerCache = new ConcurrentDictionary<int, Customer>(context.Customers.ToDictionary(c => c.CustomerId));
        }
    }

    public Task<IEnumerable<Customer>> RetrieveAllAsync()
    {
        //Get the records from the cache
        return Task.FromResult
            (customerCache is null ? Enumerable.Empty<Customer>()
            : customerCache.Values);
    }

    public Task<Customer?> RetrieveByIdAsync(int id)
    {
        if (customerCache is null) return null!;

        customerCache.TryGetValue(id, out Customer? customer);
        return Task.FromResult(customer);
    }

    public Customer UpdateCache(int id, Customer customer)
    {
        Customer? old;
        if (customerCache is not null)
        {
            if(customerCache.TryGetValue(id, out old))
            {
                if (customerCache.TryUpdate(id, customer, old))
                {
                    return customer;
                }
            }
        }
        return null!;
    }

    public async Task<Customer?> CreateAsync(Customer customer)
    {
        EntityEntry<Customer> added = await context.Customers.AddAsync(customer);
        int affected = await context.SaveChangesAsync();

        if (affected == 1)
        {
            if (customerCache is null) return customer;

            return customerCache.AddOrUpdate(customer.CustomerId, customer, UpdateCache);
        }
        else
        {
            return null;
        }
    }

    public async Task<Customer?> UpdateAsync(int id, Customer customer)
    {
        context.Customers.Update(customer);
        int affected = await context.SaveChangesAsync();

        if (affected == 1)
        {
            return UpdateCache(id, customer);
        }
        return null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Customer? customer = context.Customers.Find(id);
        if (customer is null) return null;
        context.Customers.Remove(customer);
        int affected = await context.SaveChangesAsync();
        if (affected == 1)
        {
            if (customerCache is null) return null; 
            return customerCache.TryRemove(id, out customer);

        }
        else
        {
            return null;
        }
    }
}
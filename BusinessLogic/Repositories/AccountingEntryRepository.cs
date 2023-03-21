
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;

namespace CuentasPorCobrar.Shared;

public class AccountingEntryRepository : IAccountingEntryRepository
{
    //Use a static thread safe dictionary field to cache the customer 

    private static ConcurrentDictionary<int, AccountingEntry>? accountingCache;


    /*Use an instance of data context field because it should not
     be cached due to their internal caching
    */


    private CuentasporcobrardbContext db; 

    public AccountingEntryRepository(CuentasporcobrardbContext db)
    {
        this.db=db;

        if(accountingCache is null)
        {
            accountingCache= new ConcurrentDictionary<int, AccountingEntry>(db.AccountingEntries.Include(x=>x.Customer).ToDictionary(d => d.AccountingEntryId)); 

        }
    }


    public Task<IEnumerable<AccountingEntry>> RetrieveAllAsync()
    {

        //for performance get from cache 

        

        return Task.FromResult
            (accountingCache is null ?
            Enumerable.Empty<AccountingEntry>()
            : accountingCache.Values); 

        

    }

    public  Task<AccountingEntry?> RetrieveAsync(int id)
    {


        if (accountingCache is null) return null!;


        accountingCache.TryGetValue(id, out AccountingEntry? accountingEntry);
        return Task.FromResult(accountingEntry); 
    }


    public async Task<AccountingEntry?> CreateAsync(AccountingEntry accountingEntry)
    {
        //Add to database using EF Core
        EntityEntry<AccountingEntry> added = await db.AccountingEntries.AddAsync(accountingEntry);


      
        int affected = await db.SaveChangesAsync();

        if (affected==1)
        {
            if (accountingCache is null) return accountingEntry;

            //if the accountingEntry is new, add it to cache
            await GetCustomers();
            return accountingCache.AddOrUpdate(accountingEntry.AccountingEntryId, accountingEntry, UpdateCache);
             
        }
        else
        {
            return null; 
        }

    }

    public async Task<Task<List<Customer>>> GetCustomers()
    {
        var customers =await db.Customers.ToListAsync();
        return Task.FromResult(customers); 

    }

    private AccountingEntry UpdateCache(int id, AccountingEntry accountingEntry)
    {
        AccountingEntry? old; 

        if(accountingCache is not null)
        {
            if(accountingCache.TryGetValue(id,out old))
            {
                if (accountingCache.TryUpdate(id, accountingEntry, old)) 
                {
                    return accountingEntry;

                }
            }
        }
        return null!; 
    }
    public async Task<AccountingEntry?> UpdateAsync(int id, AccountingEntry accountingEntry )
    {
        //update in database
        
        db.AccountingEntries.Update(accountingEntry);

        int affected = await db.SaveChangesAsync(); 

        if(affected==1)
        {
            //update in cache 
            await GetCustomers();
            return UpdateCache(id, accountingEntry); 
        }
        return null; 
    }


    public async Task<bool?> DeleteAsync(int id)
    {
        //remove from database 
        AccountingEntry? accountingEntry = db.AccountingEntries.Find(id);

        if (accountingEntry is null) return null;

        db.AccountingEntries.Remove(accountingEntry);

        int affected = await db.SaveChangesAsync();

        if (affected==1)
        {
            if (accountingCache is null) return null;

            //remove from cache
            return accountingCache.TryRemove(id, out accountingEntry);
        }
        else
        {
            return null;
        }


    }
   
}

    

    
    

   

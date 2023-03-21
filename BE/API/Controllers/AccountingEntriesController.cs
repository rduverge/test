using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CuentasPorCobrar.Shared;

using FluentValidation.Results;
using FluentValidation;
using API.Middleware;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountingEntriesController : ControllerBase
{
    private readonly IAccountingEntryRepository repo;
    private readonly IValidator<AccountingEntry> validator;


    //constructor injects repository registered in Program 

    public AccountingEntriesController(IAccountingEntryRepository repo, 
        IValidator<AccountingEntry> validator)
    {
        this.repo=repo;
        this.validator=validator; 
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AccountingEntry>))]
    public async Task<IEnumerable<AccountingEntry>> GetAccountingEntries()
    {
        return await repo.RetrieveAllAsync();
    }

    //GET: api/accountingEntries[id]
    [HttpGet("{id}", Name = nameof(GetAccountingEntry))]
    [ProducesResponseType(200, Type = typeof(AccountingEntry))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAccountingEntry(int id)
    {
        AccountingEntry? accountingEntry = await repo.RetrieveAsync(id);

        return accountingEntry is null ? NotFound() : Ok(accountingEntry); 
    }

    //POST: api/accountingEntries
    //Body: AccountingEntry(JSON,XML)
    [HttpPost]
    [ProducesResponseType(201,Type=typeof(AccountingEntry))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] AccountingEntry accountingEntry)
    {
        ValidationResult vadResult = await validator.ValidateAsync(accountingEntry); 
        

        if (accountingEntry is null) return BadRequest();


        if (!vadResult.IsValid)
        {
            vadResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

    

        AccountingEntry? addedEntry = await repo.CreateAsync(accountingEntry);

       
        return addedEntry is null ? BadRequest("Repository failed to create accounting entry")
            : CreatedAtRoute(routeName: nameof(GetAccountingEntry),
            routeValues: new { id = addedEntry.AccountingEntryId },
            value: addedEntry); 
    }


    //PUT: api/accountingEntries/[id]
    //BODY:AccountingEntry(JSON,XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] AccountingEntry accountingEntry)
    {
        ValidationResult vadResult = await validator.ValidateAsync(accountingEntry);

        if (!vadResult.IsValid)
        {
            vadResult.AddToModelState(ModelState);
            return BadRequest(ModelState); 
        }
        if (accountingEntry is null || accountingEntry.AccountingEntryId!=id) return BadRequest();

        AccountingEntry? existing = await repo.RetrieveAsync(id);

        if (existing is null) return NotFound();
        await repo.UpdateAsync(id, accountingEntry);

        return new NoContentResult(); //204 No content 
    }

    //DELETE: api/accountingEntries/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        AccountingEntry? existing = await repo.RetrieveAsync(id);
        if (existing is null) return NotFound();

        bool? deleted = await repo.DeleteAsync(id);

        //SHORT CIRCUIT AND 
        return deleted.HasValue && deleted.Value ?
            new NoContentResult()
            : BadRequest($"Document {id} was found but failed to delete"); 
        
    }

}
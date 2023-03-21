using API.Middleware;

using CuentasPorCobrar.Shared;
using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository repo; 
    private readonly IValidator<Transaction> validator; 
    
    public TransactionController(ITransactionRepository repo, IValidator<Transaction> validator)
    {
        this.repo = repo;
        this.validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(200, Type =typeof(IEnumerable<Transaction>))]
    public async Task<IEnumerable<Transaction>> GetTransactions()
    {
        return await repo.RetrieveAllAsync();
    }

    //GET: api/transactions/[id]
    [HttpGet("{id}", Name=nameof(GetTransactionByID))]
    [ProducesResponseType(200, Type=typeof(Transaction))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTransactionByID(int id)
    {
        Transaction? transaction = await repo.RetrieveByIdAsync(id);
        return transaction is null ? NotFound() : Ok(transaction);
    }

    //Create a new Transaction
    //POST: api/transactions/[id]
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Transaction))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Transaction transaction)
    {
        ValidationResult result = await validator.ValidateAsync(transaction);
        
        if (transaction is null) return BadRequest();
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        Transaction? addedTransaction = await repo.CreateAsync(transaction);

        return addedTransaction is null? BadRequest("Error to save the new Transaction") 
            : CreatedAtRoute(routeName:nameof(GetTransactionByID),
            routeValues: new {id = addedTransaction.TransactionId},
            value: addedTransaction);
    }
    
    //Updates the selected transaction
    //PUT: api/transactions/[id]
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] Transaction transaction)
    {
        ValidationResult result = await validator.ValidateAsync(transaction);
        
        if(transaction is null || transaction.TransactionId != id) return BadRequest();

        if(!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        Transaction? existing = await repo.RetrieveByIdAsync(id);

        if(existing is null) return NotFound();
        await repo.UpdateAsync(id, transaction);

        return new NoContentResult();
    }

    //DELETE: api/transactions/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        Transaction? existing = await repo.RetrieveByIdAsync(id);
        if (existing is null) return NotFound();

        bool? deleted = await repo.DeleteAsync(id);

        return deleted.HasValue && deleted.Value ?
            new NoContentResult()
            : BadRequest($"Transaction number {id} was found but failed to delete.");
    }
}

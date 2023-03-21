

using CuentasPorCobrar.Shared;
using FluentValidation;

namespace CuentasPorCobrar.Shared;

public class TransactionValidator:AbstractValidator<Transaction>
{
   public TransactionValidator()
    {
       

        RuleFor(t => t.Amount)
            .GreaterThan(0)
            .WithMessage("It cannot negative")
            .NotEmpty()
            .WithMessage("It cannot be empty"); 
            
            
    }
    
}
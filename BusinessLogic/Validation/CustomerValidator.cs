

using CuentasPorCobrar.Shared;
using FluentValidation;

namespace CuentasPorCobrar.Shared;

public class CustomerValidator:AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Name)
             .NotEmpty()
             .WithMessage("It cannot be empty.")
             .MinimumLength(2)
             .WithMessage
             ("The minimum length should be greater than 2")
             .MaximumLength(40)
             .WithMessage("The maximum length should be 40");

        RuleFor(c => c.CreditLimit)
            .GreaterThan(0)
            .WithMessage("It cannot be negative or zero");

        RuleFor(c => c.Identification)
            .NotEmpty()
            .WithMessage("Cannot be empty.")
            .MaximumLength(13)
            .WithMessage("It cannot be greater than 13")
            .Must(identification=> CedulaValidator.ValidaCedula(identification)is true);


        
            



            
       
    }

}


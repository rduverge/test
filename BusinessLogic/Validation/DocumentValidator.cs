
using CuentasPorCobrar.Shared;
using FluentValidation;

namespace CuentasPorCobrar.Shared;
public class DocumentValidator: AbstractValidator<Document>
{

    public DocumentValidator()
    {
        RuleFor(d => d.Description)
            .MinimumLength(10)
            .WithMessage($"The description you have written should be longer")
            .MaximumLength(50)
            .WithMessage("The description you have written passed the limit")
            .NotEmpty()
            .WithMessage("The document description cannot be empty");


        RuleFor(d => d.LedgerAccount)
            .NotEmpty()
            .WithMessage("It cannot be empty"); 


    }



}


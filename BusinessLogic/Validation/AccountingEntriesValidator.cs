

using CuentasPorCobrar.Shared;
using FluentValidation;

namespace CuentasPorCobrar.Shared;

public class AccountingEntriesValidator:AbstractValidator<AccountingEntry>
{
    public AccountingEntriesValidator()
    {
        RuleFor(a => a.Description)
            .NotEmpty()
            .WithMessage("It cannot be empty")
            .MinimumLength(15)
            .WithMessage("It cannot be less than 15 digits")
            .MaximumLength(150)
            .WithMessage("It cannot be greater than 150 digits")
            ;

        RuleFor(a => a.Account)
            .NotEmpty()
            .WithMessage("It cannot be empty");

        RuleFor(a => a.AccountEntryAmount)
            .GreaterThan(0)
            .WithMessage("It cannot be negative")
            .NotEmpty()
            .WithMessage("It cannot be empty");

        RuleFor(a => a.Account)
            .NotEmpty()
            .WithMessage("It cannot be empty"); 

       

    }
}


using FluentValidation;
using PhoneNumbers;
using System.Text.RegularExpressions;
namespace Mc2.CrudTest.Application.Customers.Commands.Create;

public partial class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
  
        RuleFor(v => v.PhoneNumber!)
             .NotEmpty()
             .Must(BeValidMobileNumber).WithMessage("Invalid mobile phone number");

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email address");

       RuleFor(v => v.BankAccountNumber!)
            .NotEmpty()
            .Must(BeValidBankAccountNumber).WithMessage("Invalid bank account number");
    }

    private bool BeValidMobileNumber(string phoneNumber)
    {
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
        try
        {
            //+13478093374
            PhoneNumber numberProto = phoneUtil.Parse(phoneNumber, "US");
            return phoneUtil.IsValidNumber(numberProto);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    private bool BeValidBankAccountNumber(string bankAccountNumber)
    {
        // Format: 10 digits
        return BankAccountRegex().IsMatch(bankAccountNumber);
    }

    [GeneratedRegex("^\\d{10}$")]
    private static partial Regex BankAccountRegex();

}

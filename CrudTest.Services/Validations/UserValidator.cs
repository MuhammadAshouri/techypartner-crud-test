using CrudTest.Domain.Dtos;
using FluentValidation;

namespace CrudTest.Services.Validations;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(c => c.FirstName.Trim()).MinimumLength(2).MaximumLength(60).WithMessage("Firstname should be at least 2 characters");
        RuleFor(c => c.LastName.Trim()).MinimumLength(2).MaximumLength(60).WithMessage("Lastname should be at least 2 characters");
        RuleFor(c => c.Email.Trim()).NotNull().EmailAddress().WithMessage("Email is invalid");
        RuleFor(c => c.Gender.Trim().ToLower()).NotNull().Matches("^((male)|(female)|(others))$").WithMessage("Gender is invalid");
    }
}

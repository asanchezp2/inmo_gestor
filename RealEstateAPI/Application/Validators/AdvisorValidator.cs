using FluentValidation;
using RealEstateAPI.Application.DTOs;
using System.Text.RegularExpressions;

namespace RealEstateAPI.Application.Validators;

public class AdvisorCreateValidator : AbstractValidator<AdvisorCreateDTO>
{
    public AdvisorCreateValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format");

        RuleFor(x => x.PrimaryPhone)
            .NotEmpty().WithMessage("Primary phone is required")
            .MaximumLength(20).WithMessage("Primary phone cannot exceed 20 characters");

        RuleFor(x => x.SecondaryPhone)
            .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.SecondaryPhone))
            .WithMessage("Secondary phone cannot exceed 20 characters");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.PrimaryPhone) || !string.IsNullOrEmpty(x.SecondaryPhone))
            .WithMessage("At least one phone number is required");
    }
}

public class AdvisorUpdateValidator : AbstractValidator<AdvisorUpdateDTO>
{
    public AdvisorUpdateValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format");

        RuleFor(x => x.PrimaryPhone)
            .NotEmpty().WithMessage("Primary phone is required")
            .MaximumLength(20).WithMessage("Primary phone cannot exceed 20 characters");

        RuleFor(x => x.SecondaryPhone)
            .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.SecondaryPhone))
            .WithMessage("Secondary phone cannot exceed 20 characters");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.PrimaryPhone) || !string.IsNullOrEmpty(x.SecondaryPhone))
            .WithMessage("At least one phone number is required");
    }
}

using FluentValidation;
using RealEstateAPI.Application.DTOs;
using RealEstateAPI.Domain.Enums;

namespace RealEstateAPI.Application.Validators;

public class PropertyCreateValidator : AbstractValidator<PropertyCreateDTO>
{
    public PropertyCreateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

        RuleFor(x => x.AvailableDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Available date cannot be in the past");

        RuleFor(x => x.ImageUrls)
            .Must(x => x.Count <= 10)
            .WithMessage("Maximum 10 images allowed");

        RuleFor(x => x.AdvisorId)
            .GreaterThan(0).WithMessage("Valid advisor is required");
    }
}

public class PropertyUpdateValidator : AbstractValidator<PropertyUpdateDTO>
{
    public PropertyUpdateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

        RuleFor(x => x.ImageUrls)
            .Must(x => x.Count <= 10)
            .WithMessage("Maximum 10 images allowed");
    }
}

public class PropertyStatusUpdateValidator : AbstractValidator<PropertyStatusUpdateDTO>
{
    public PropertyStatusUpdateValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value");
    }
}

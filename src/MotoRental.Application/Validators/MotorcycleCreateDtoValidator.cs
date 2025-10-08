using FluentValidation;
using MotoRental.Application.DTOs;

namespace MotoRental.Application.Validators
{
    public class MotorcycleCreateDtoValidator : AbstractValidator<MotorcycleCreateDto>
    {
        public MotorcycleCreateDtoValidator()
        {
            RuleFor(x => x.Year)
                .GreaterThan(1900).WithMessage("Year is invalid.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(200);

            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("Plate is required.")
                .MaximumLength(10);
        }
    }
}

using FluentValidation;
using MotoRental.Application.DTOs;
using System;

namespace MotoRental.Application.Validators
{
    public class RiderCreateDtoValidator : AbstractValidator<RiderCreateDto>
    {
        public RiderCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Cnpj).NotEmpty().Length(14).WithMessage("CNPJ must have 14 digits (numbers only).");
            RuleFor(x => x.BirthDate).LessThan(DateTime.UtcNow).WithMessage("Birthdate must be in the past.");
            RuleFor(x => x.CnhNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CnhType).NotEmpty()
                 .Must(t => t == "A" || t == "B" || t == "AB")
                 .WithMessage("CNH type must be 'A', 'B' or 'AB'.");
        }
    }
}

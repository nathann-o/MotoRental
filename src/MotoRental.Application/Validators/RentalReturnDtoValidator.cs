using FluentValidation;
using MotoRental.Application.DTOs;

namespace MotoRental.Application.Validators
{
    public class RentalReturnDtoValidator : AbstractValidator<RentalReturnDto>
    {
        public RentalReturnDtoValidator()
        {
            RuleFor(x => x.RentalId).NotEmpty();
            RuleFor(x => x.ActualReturnDate).NotEmpty();
        }
    }
}

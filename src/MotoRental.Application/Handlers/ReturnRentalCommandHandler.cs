using System;
using System.Threading;
using System.Threading.Tasks;
using MotoRental.Application.DTOs;


namespace MotoRental.Application.Handlers
{
    public class ReturnRentalCommandHandler
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IRentalDomainService _rentalDomainService;

        public ReturnRentalCommandHandler(IRentalRepository rentalRepository, IRentalDomainService rentalDomainService)
        {
            _rentalRepository = rentalRepository;
            _rentalDomainService = rentalDomainService;
        }

        public async Task<RentalChargeResultDto> HandleAsync(RentalReturnDto dto, CancellationToken ct = default)
        {
            var rental = await _rentalRepository.GetActiveByIdAsync(dto.RentalId, ct);
            if (rental == null) throw new DomainException("Active rental not found.");

            var actualReturn = dto.ActualReturnDate.Date;

            // compute charge using domain service
            var total = _rentalDomainService.ComputeCharge(rental.Plan, rental.StartDate, rental.ExpectedEndDate, actualReturn);

            // compute breakdown values (we'll compute same way as DomainService)
            var daily = rental.Plan.DailyRate();
            var plannedDays = (int)rental.Plan;
            int usedDays = 0;
            int extraDays = 0;
            int unusedDays = 0;
            decimal baseCharge = 0m;
            decimal penalty = 0m;
            decimal lateFee = 0m;

            if (actualReturn < rental.ExpectedEndDate.Date)
            {
                usedDays = (actualReturn - rental.StartDate.Date).Days + 1;
                if (usedDays < 0) usedDays = 0;
                unusedDays = plannedDays - usedDays;
                if (unusedDays < 0) unusedDays = 0;
                baseCharge = usedDays * daily;
                decimal penaltyPct = rental.Plan == Plan.Days7 ? 0.2m : rental.Plan == Plan.Days15 ? 0.4m : 0m;
                penalty = penaltyPct * (unusedDays * daily);
            }
            else if (actualReturn > rental.ExpectedEndDate.Date)
            {
                usedDays = plannedDays;
                extraDays = (actualReturn - rental.ExpectedEndDate.Date).Days;
                baseCharge = plannedDays * daily;
                lateFee = extraDays * 50m;
            }
            else
            {
                usedDays = plannedDays;
                baseCharge = plannedDays * daily;
            }

            var result = new RentalChargeResultDto
            {
                BaseCharge = baseCharge,
                Penalty = penalty,
                LateFee = lateFee,
                Total = total,
                UsedDays = usedDays,
                ExtraDays = extraDays,
                UnusedDays = unusedDays
            };

            // close rental and persist
            rental.Close(actualReturn);
            await _rentalRepository.SaveChangesAsync(ct);

            return result;
        }
    }
}

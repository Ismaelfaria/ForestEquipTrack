using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;

namespace BusOnTime.Application.Validators
{
    public class EquipmentModelStateHourlyEarningsInputValidator : AbstractValidator<EquipmentModelStateHourlyEarningsIM>
    {
        public EquipmentModelStateHourlyEarningsInputValidator()
        {
            RuleFor(e => e.Value).NotEmpty().WithMessage("Preencha o campo 'Valor'.");
        }
    }
}

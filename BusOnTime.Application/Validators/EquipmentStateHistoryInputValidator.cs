using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;

namespace BusOnTime.Application.Validators
{
    public class EquipmentStateHistoryInputValidator : AbstractValidator<EquipmentStateHistoryIM>
    {
        public EquipmentStateHistoryInputValidator()
        {
            RuleFor(e => e.Date).NotEmpty().WithMessage("Preencha o campo 'Data'.");
        }
    }
}

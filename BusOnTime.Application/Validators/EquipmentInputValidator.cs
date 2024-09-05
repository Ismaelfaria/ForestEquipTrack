using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;

namespace BusOnTime.Application.Validators
{
    public class EquipmentInputValidator : AbstractValidator<EquipmentIM>
    {
        public EquipmentInputValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Preencha o campo 'Nome'.");
        }
    }
}

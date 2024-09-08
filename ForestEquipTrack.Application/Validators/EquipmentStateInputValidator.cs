using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using FluentValidation;

namespace ForestEquipTrack.Application.Validators
{
    public class EquipmentStateInputValidator : AbstractValidator<EquipmentStateIM>
    {
        public EquipmentStateInputValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Preencha o campo 'Nome'.");
            RuleFor(e => e.Color).NotEmpty().WithMessage("Preencha o campo 'Cor'.");
        }
    }
}

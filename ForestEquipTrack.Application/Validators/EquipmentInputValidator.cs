using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using FluentValidation;

namespace ForestEquipTrack.Application.Validators
{
    public class EquipmentInputValidator : AbstractValidator<EquipmentIM>
    {
        public EquipmentInputValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Preencha o campo 'Nome'.");
        }
    }
}

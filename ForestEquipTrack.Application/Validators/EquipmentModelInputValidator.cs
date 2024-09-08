using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using FluentValidation;

namespace ForestEquipTrack.Application.Validators
{
    public class EquipmentModelInputValidator : AbstractValidator<EquipmentModelIM>
    {
        public EquipmentModelInputValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Preencha o campo 'Nome'.");
        }
    }
}

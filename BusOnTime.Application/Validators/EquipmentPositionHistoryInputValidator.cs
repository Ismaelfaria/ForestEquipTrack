using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using FluentValidation;

namespace ForestEquipTrack.Application.Validators
{
    public class EquipmentPositionHistoryInputValidator : AbstractValidator<EquipmentPositionHistoryIM>
    {
        public EquipmentPositionHistoryInputValidator()
        {
            RuleFor(e => e.Date).NotEmpty().WithMessage("Preencha o campo 'Data'.");
            RuleFor(e => e.Lat).NotEmpty().WithMessage("Preencha o campo 'Latitude'.");
            RuleFor(e => e.Lon).NotEmpty().WithMessage("Preencha o campo 'Longitude'.");
        }
    }
}

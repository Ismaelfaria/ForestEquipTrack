using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Validators
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

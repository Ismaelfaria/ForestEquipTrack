using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Validators
{
    public class EquipmentStateHistoryInputValidator : AbstractValidator<EquipmentStateHistoryIM>
    {
        public EquipmentStateHistoryInputValidator()
        {
            RuleFor(e => e.EquipmentId).NotEmpty().WithMessage("Preencha o campo 'Id do equipamento'.");
            RuleFor(e => e.Date).NotEmpty().WithMessage("Preencha o campo 'Data'.");
        }
    }
}

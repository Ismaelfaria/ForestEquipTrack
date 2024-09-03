using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

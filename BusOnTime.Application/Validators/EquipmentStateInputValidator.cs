using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Validators
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

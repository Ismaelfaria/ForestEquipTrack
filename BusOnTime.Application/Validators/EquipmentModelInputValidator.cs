using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Validators
{
    public class EquipmentModelInputValidator : AbstractValidator<EquipmentModelIM>
    {
        public EquipmentModelInputValidator()
        {
            RuleFor(e => e.EquipmentId).NotEmpty().WithMessage("Preencha o campo 'Id do equipamento'.");
            RuleFor(e => e.Name).NotEmpty().WithMessage("Preencha o campo 'Nome'.");
        }
    }
}

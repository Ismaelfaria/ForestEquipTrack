using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Mapping.DTOs.InputModel
{
    public class EquipmentModelStateHourlyEarningsIM
    {
        public Guid? EquipmentModelId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public decimal Value { get; set; }
    }
}

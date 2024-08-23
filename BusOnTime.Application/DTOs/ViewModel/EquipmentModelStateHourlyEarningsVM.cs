using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.DTOs.ViewModel
{
    public class EquipmentModelStateHourlyEarningsVM
    {
        public Guid EquipmentModelStateHourlyEarningsId { get; set; }
        public Guid EquipmentModelId { get; set; }
        public Guid EquipmentStateId { get; set; }
        public decimal Value { get; set; }
        public EquipmentModel? EquipmentModel { get; set; }
        public EquipmentState? EquipmentState { get; set; }
    }
}

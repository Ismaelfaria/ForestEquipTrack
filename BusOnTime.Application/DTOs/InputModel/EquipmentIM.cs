using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.DTOs.InputModel
{
    public class EquipmentIM
    {
        public Guid EquipmentModelId { get; set; }
        public string? Name { get; set; }
        public EquipmentModel? EquipmentModel { get; set; }
        public ICollection<EquipmentStateHistory>? EquipmentStateHistories { get; set; }
        public ICollection<EquipmentPositionHistory>? EquipmentPositionHistories { get; set; }
    }
}

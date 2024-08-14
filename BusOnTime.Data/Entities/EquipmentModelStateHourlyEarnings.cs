using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class EquipmentModelStateHourlyEarnings
    {
        public Guid EquipmentModelId { get; set; }
        public Guid EquipmentStateId { get; set; }
        public decimal Value { get; set; }
        public EquipmentModel EquipmentModel { get; set; }
        public EquipmentState EquipmentState { get; set; }
    }
}

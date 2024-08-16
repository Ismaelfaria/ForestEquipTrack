using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class EquipmentModelStateHourlyEarnings
    {
        public EquipmentModelStateHourlyEarnings()
        {
            IsDeleted = false;
        }
        public Guid EquipmentModelStateHourlyEarningsId { get; set; }
        public Guid EquipmentModelId { get; set; }
        public Guid EquipmentStateId { get; set; }
        public decimal Value { get; set; }
        public bool IsDeleted { get; set; }
        public EquipmentModel EquipmentModel { get; set; }
        public EquipmentState EquipmentState { get; set; }
    }
}

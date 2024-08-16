using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusOnTime.Data.Entities.Generic;

namespace BusOnTime.Data.Entities
{
    public class EquipmentModelStateHourlyEarnings : BaseEntity
    {
        public EquipmentModelStateHourlyEarnings()
        {
            IsDeleted = false;
        }
        public Guid EquipmentModelStateHourlyEarningsId { get; set; }
        public Guid EquipmentModelId { get; set; }
        public Guid EquipmentStateId { get; set; }
        public decimal Value { get; set; }
        public EquipmentModel? EquipmentModel { get; set; }
        public EquipmentState? EquipmentState { get; set; }
    }
}

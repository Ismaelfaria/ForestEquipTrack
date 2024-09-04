using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusOnTime.Data.Entities.Generic;

namespace BusOnTime.Data.Entities
{
    public class EquipmentStateHistory : BaseEntity
    {
        public EquipmentStateHistory()
        {
            IsDeleted = false;
        }
        public Guid EquipmentStateHistoryId { get; set; }
        public Guid? EquipmentId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public DateTime Date { get; set; }
        public Equipment? Equipment { get; set; }
        public EquipmentState? EquipmentState { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class EquipmentStateHistory
    {
        public EquipmentStateHistory()
        {
            IsDeleted = false;
        }
        public Guid EquipmentStateId { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public Equipment Equipment { get; set; }
        public EquipmentState EquipmentState { get; set; }
    }
}

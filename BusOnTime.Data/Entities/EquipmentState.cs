using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class EquipmentState
    {
        public EquipmentState()
        {
            IsDeleted = false;
        }
        public Guid StateId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<EquipmentModelStateHourlyEarnings> EquipmentModelStateHourlyEarnings { get; set; }
        public ICollection<EquipmentStateHistory> EquipmentStateHistories { get; set; }
    }
}

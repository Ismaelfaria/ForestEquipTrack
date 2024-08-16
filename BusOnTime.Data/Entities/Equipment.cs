using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class Equipment
    {
        public Equipment()
        {
            IsDeleted = false;
        }
        public Guid EquipmentId { get; set; }
        public Guid EquipmentModelId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public EquipmentModel EquipmentModel { get; set; }
        public ICollection<EquipmentStateHistory> EquipmentStateHistories { get; set; }
        public ICollection<EquipmentPositionHistory> EquipmentPositionHistories { get; set; }

    }
}

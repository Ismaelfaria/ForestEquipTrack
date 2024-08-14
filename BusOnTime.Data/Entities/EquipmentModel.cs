using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class EquipmentModel
    {
        public Guid ModelId { get; set; }
        public Guid EquipmentId { get; set; }
        public string Name { get; set; }
        public ICollection<Equipment> Equipment { get; set; }
        public ICollection<EquipmentModelStateHourlyEarnings> EquipmentModelStateHourlyEarnings { get; set; }

    }
}

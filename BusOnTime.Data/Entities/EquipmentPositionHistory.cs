using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusOnTime.Data.Entities.Generic;

namespace BusOnTime.Data.Entities
{
    public class EquipmentPositionHistory : BaseEntity
    {
        public EquipmentPositionHistory()
        {
            IsDeleted = false;
        }
        public Guid EquipmentPositionId { get; set; }
        public Guid? EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public Equipment? Equipment { get; set; }
    }
}

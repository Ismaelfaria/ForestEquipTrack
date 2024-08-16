using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class EquipmentPositionHistory
    {
        public EquipmentPositionHistory()
        {
            IsDeleted = false;
        }
        public Guid EquipmentPositionId { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public int Lat { get; set; }
        public int Lon { get; set; }
        public bool IsDeleted { get; set; }
        public Equipment Equipment { get; set; }
    }
}

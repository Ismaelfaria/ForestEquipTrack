using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Entities
{
    public class EquipmentPositionHistory
    {
        public Guid EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public int Lat { get; set; }
        public int Lon { get; set; }
        public Equipment Equipment { get; set; }
    }
}

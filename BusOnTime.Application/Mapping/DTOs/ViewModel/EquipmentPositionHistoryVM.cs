using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentPositionHistoryVM
    {
        public Guid EquipmentPositionId { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public int Lat { get; set; }
        public int Lon { get; set; }
        public Equipment? Equipment { get; set; }
    }
}

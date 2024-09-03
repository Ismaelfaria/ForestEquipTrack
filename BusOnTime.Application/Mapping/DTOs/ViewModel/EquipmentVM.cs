using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentVM
    {
        public Guid EquipmentId { get; set; }
        public Guid? EquipmentModelId { get; set; }
        public string? Name { get; set; }
    }
}

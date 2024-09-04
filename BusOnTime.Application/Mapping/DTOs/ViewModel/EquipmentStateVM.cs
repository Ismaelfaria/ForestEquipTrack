using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentStateVM
    {
        public Guid EquipmentStateId { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
    }
}

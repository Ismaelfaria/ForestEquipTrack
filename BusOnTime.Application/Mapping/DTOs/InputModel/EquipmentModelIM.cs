using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Mapping.DTOs.InputModel
{
    public class EquipmentModelIM
    {
        public Guid? EquipmentId { get; set; }
        public string? Name { get; set; }
    }
}

using BusOnTime.Application.Interfaces.Generic;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Interfaces
{
    public interface IEquipmentModelStateHourlyEarningS : IServiceBase<EquipmentModelStateHourlyEarnings>
    {
        Task<EquipmentModelStateHourlyEarnings> CreateAsync(EquipmentModelStateHourlyEarningsIM entity);
    }
}

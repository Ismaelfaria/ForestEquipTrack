﻿using BusOnTime.Data.DataContext;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Data.Repositories.Concrete
{
    internal class EquipmentPositionHistoryR : RepositoryBase<EquipmentPositionHistory>
    {
        public EquipmentPositionHistoryR(Context context) : base(context)
        {}
    }
}

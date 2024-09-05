using BusOnTime.Domain.Entities.Generic;

namespace BusOnTime.Domain.Entities
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

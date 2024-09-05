using BusOnTime.Domain.Entities.Generic;

namespace BusOnTime.Domain.Entities
{
    public class EquipmentStateHistory : BaseEntity
    {
        public EquipmentStateHistory()
        {
            IsDeleted = false;
            EquipmentStateHistoryId = Guid.NewGuid();
        }
        public Guid EquipmentStateHistoryId { get; set; }
        public Guid? EquipmentId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public DateTime Date { get; set; }
        public Equipment? Equipment { get; set; }
        public EquipmentState? EquipmentState { get; set; }
    }
}

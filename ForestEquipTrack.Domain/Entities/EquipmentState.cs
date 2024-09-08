using ForestEquipTrack.Domain.Entities.Generic;

namespace ForestEquipTrack.Domain.Entities
{
    public class EquipmentState : BaseEntity
    {
        
        public Guid EquipmentStateId { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public ICollection<EquipmentModelStateHourlyEarnings>? EquipmentModelStateHourlyEarnings { get; set; }
        public ICollection<EquipmentStateHistory>? EquipmentStateHistories { get; set; }
    }
}

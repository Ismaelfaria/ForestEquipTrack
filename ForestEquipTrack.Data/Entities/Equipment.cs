using ForestEquipTrack.Domain.Entities.Generic;

namespace ForestEquipTrack.Domain.Entities
{
    public class Equipment : BaseEntity
    {
        public Equipment()
        {
            IsDeleted = false;
        }
        public Guid EquipmentId { get; set; }
        public Guid? EquipmentModelId { get; set; }
        public string? Name { get; set; }
        public EquipmentModel? EquipmentModel { get; set; }
        public ICollection<EquipmentStateHistory>? EquipmentStateHistories { get; set; }
        public ICollection<EquipmentPositionHistory>? EquipmentPositionHistories { get; set; }

    }
}

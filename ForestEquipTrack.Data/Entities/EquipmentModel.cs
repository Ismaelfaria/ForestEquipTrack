using ForestEquipTrack.Domain.Entities.Generic;

namespace ForestEquipTrack.Domain.Entities
{
    public class EquipmentModel : BaseEntity
    {
        public EquipmentModel()
        {
            IsDeleted = false;
        }
        public Guid EquipmentModelId { get; set; }
        public Guid? EquipmentId { get; set; }
        public string? Name { get; set; }
        public ICollection<Equipment>? Equipment { get; set; }
        public ICollection<EquipmentModelStateHourlyEarnings>? EquipmentModelStateHourlyEarnings { get; set; }

    }
}

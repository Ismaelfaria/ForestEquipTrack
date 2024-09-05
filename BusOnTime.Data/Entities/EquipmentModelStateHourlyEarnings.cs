using BusOnTime.Domain.Entities.Generic;

namespace BusOnTime.Domain.Entities
{
    public class EquipmentModelStateHourlyEarnings : BaseEntity
    {
        public EquipmentModelStateHourlyEarnings()
        {
            IsDeleted = false;
        }
        public Guid EquipmentModelStateHourlyEarningsId { get; set; }
        public Guid? EquipmentModelId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public decimal Value { get; set; }
        public EquipmentModel? EquipmentModel { get; set; }
        public EquipmentState? EquipmentState { get; set; }
    }
}

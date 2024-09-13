using ForestEquipTrack.Domain.Entities.Enum;
using ForestEquipTrack.Domain.Entities.Generic;
using System.Text.Json.Serialization;

namespace ForestEquipTrack.Domain.Entities
{
    public class EquipmentModelStateHourlyEarnings : BaseEntity
    {
        public EquipmentModelStateHourlyEarnings()
        {
            IsDeleted = false;
            EquipmentModelStateHourlyEarningsId = Guid.NewGuid();
        }
        public Guid EquipmentModelStateHourlyEarningsId { get; set; }
        public Guid EquipmentModelId { get; set; }
        public string? ModelName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquipmentStateType? Status { get; set; }
        public decimal Value { get; set; }
        public EquipmentModel? EquipmentModel { get; set; }
    }
}

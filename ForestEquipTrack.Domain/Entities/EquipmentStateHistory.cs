using ForestEquipTrack.Domain.Entities.Enum;
using ForestEquipTrack.Domain.Entities.Generic;
using System.Text.Json.Serialization;

namespace ForestEquipTrack.Domain.Entities
{
    public class EquipmentStateHistory : BaseEntity
    {
        public EquipmentStateHistory()
        {
            IsDeleted = false;
            EquipmentStateHistoryId = Guid.NewGuid();
        }
        public Guid EquipmentStateHistoryId { get; set; }
        public Guid EquipmentId { get; set; }
        public string? EquipmentName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquipmentStateType? Status { get; set; }
        public DateTime Date { get; set; }
        public Equipment? Equipment { get; set; }
    }
}

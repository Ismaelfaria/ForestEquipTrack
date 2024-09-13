using ForestEquipTrack.Domain.Entities.Enum;
using System.Text.Json.Serialization;

namespace ForestEquipTrack.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentStateHistoryVM
    {
        public Guid? EquipmentStatehistoryId { get; set; }
        public Guid? EquipmentId { get; set; }
        public string? EquipmentName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquipmentStateType? Status { get; set; }
        public DateTime Date { get; set; }
    }
}

using ForestEquipTrack.Domain.Entities.Enum;
using System.Text.Json.Serialization;

namespace ForestEquipTrack.Application.Mapping.DTOs.InputModel
{
    public class EquipmentStateHistoryIM
    {
        public Guid EquipmentId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquipmentStateType? Status { get; set; }
        public DateTime Date { get; set; }
    }
}

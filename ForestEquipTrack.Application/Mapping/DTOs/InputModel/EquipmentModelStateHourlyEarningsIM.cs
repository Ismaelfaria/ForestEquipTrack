using ForestEquipTrack.Domain.Entities.Enum;
using System.Text.Json.Serialization;

namespace ForestEquipTrack.Application.Mapping.DTOs.InputModel
{
    public class EquipmentModelStateHourlyEarningsIM
    {
        public Guid EquipmentModelId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquipmentStateType? Status { get; set; }
        public decimal Value { get; set; }
    }
}

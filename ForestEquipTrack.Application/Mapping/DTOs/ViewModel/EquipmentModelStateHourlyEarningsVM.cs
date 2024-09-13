using ForestEquipTrack.Domain.Entities.Enum;
using System.Text.Json.Serialization;

namespace ForestEquipTrack.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentModelStateHourlyEarningsVM
    {
        public Guid EquipmentModelStateHourlyEarningsId { get; set; }
        public Guid? EquipmentModelId { get; set; }
        public string? ModelName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquipmentStateType? Status { get; set; }
        public decimal Value { get; set; }
 
    }
}

namespace BusOnTime.Application.Mapping.DTOs.InputModel
{
    public class EquipmentModelStateHourlyEarningsIM
    {
        public Guid? EquipmentModelId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public decimal Value { get; set; }
    }
}

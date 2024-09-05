namespace BusOnTime.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentModelStateHourlyEarningsVM
    {
        public Guid EquipmentModelStateHourlyEarningsId { get; set; }
        public Guid? EquipmentModelId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public decimal Value { get; set; }
 
    }
}

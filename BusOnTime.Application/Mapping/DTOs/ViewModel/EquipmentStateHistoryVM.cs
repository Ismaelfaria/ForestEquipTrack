namespace BusOnTime.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentStateHistoryVM
    {
        public Guid? EquipmentStatehistoryId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public Guid? EquipmentId { get; set; }
        public DateTime Date { get; set; }
    }
}

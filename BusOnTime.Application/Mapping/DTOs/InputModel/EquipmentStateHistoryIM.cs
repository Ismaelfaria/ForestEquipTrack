namespace BusOnTime.Application.Mapping.DTOs.InputModel
{
    public class EquipmentStateHistoryIM
    {
        public Guid? EquipmentId { get; set; }
        public Guid? EquipmentStateId { get; set; }
        public DateTime Date { get; set; }
    }
}

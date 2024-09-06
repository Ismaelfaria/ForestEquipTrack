namespace ForestEquipTrack.Application.Mapping.DTOs.ViewModel
{
    public class EquipmentPositionHistoryVM
    {
        public Guid EquipmentPositionId { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}

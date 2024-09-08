namespace ForestEquipTrack.Application.Mapping.DTOs.InputModel
{
    public class EquipmentPositionHistoryIM
    {
        public Guid? EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}

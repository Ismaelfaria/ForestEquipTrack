using AutoMapper;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using ForestEquipTrack.Domain.Entities;

namespace ForestEquipTrack.Application.Mapping.Profiles
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            //Input Model
            CreateMap<Equipment, EquipmentIM>().ReverseMap();
            CreateMap<EquipmentModel, EquipmentModelIM>().ReverseMap();
            CreateMap<EquipmentStateHistory, EquipmentStateHistoryIM>().ReverseMap();
            CreateMap<EquipmentPositionHistory, EquipmentPositionHistoryIM>().ReverseMap();
            CreateMap<EquipmentModelStateHourlyEarnings, EquipmentModelStateHourlyEarningsIM>().ReverseMap();
            //View Model
            CreateMap<Equipment, EquipmentVM>().ReverseMap();
            CreateMap<EquipmentModel, EquipmentModelVM>().ReverseMap();
            CreateMap<EquipmentStateHistory, EquipmentStateHistoryVM>().ReverseMap();
            CreateMap<EquipmentPositionHistory, EquipmentPositionHistoryVM>().ReverseMap();
            CreateMap<EquipmentModelStateHourlyEarnings, EquipmentModelStateHourlyEarningsVM>().ReverseMap();
        }
    }
}

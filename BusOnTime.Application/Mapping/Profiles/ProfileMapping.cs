using AutoMapper;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Domain.Entities;

namespace BusOnTime.Application.Mapping.Profiles
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            //Input Model
            CreateMap<Equipment, EquipmentIM>().ReverseMap();
            CreateMap<EquipmentModel, EquipmentModelIM>().ReverseMap();
            CreateMap<EquipmentState, EquipmentStateIM>().ReverseMap();
            CreateMap<EquipmentStateHistory, EquipmentStateHistoryIM>().ReverseMap();
            CreateMap<EquipmentPositionHistory, EquipmentPositionHistoryIM>().ReverseMap();
            CreateMap<EquipmentModelStateHourlyEarnings, EquipmentModelStateHourlyEarningsIM>().ReverseMap();
            //View Model
            CreateMap<Equipment, EquipmentVM>().ReverseMap();
            CreateMap<EquipmentModel, EquipmentModelVM>().ReverseMap();
            CreateMap<EquipmentState, EquipmentStateVM>().ReverseMap();
            CreateMap<EquipmentStateHistory, EquipmentStateHistoryVM>().ReverseMap();
            CreateMap<EquipmentPositionHistory, EquipmentPositionHistoryVM>().ReverseMap();
            CreateMap<EquipmentModelStateHourlyEarnings, EquipmentModelStateHourlyEarningsVM>().ReverseMap();
        }
    }
}

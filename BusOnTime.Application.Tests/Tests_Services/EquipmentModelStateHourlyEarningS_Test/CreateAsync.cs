using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentModelStateHourlyEarningS_Test
{
    public class CreateAsync
    {
        [Fact]
        public async void CreateAsync_ValidEquipmentModelStateHourlyEarning_ReturnsCreatedEquipmentModelStateHourlyEarning()
        {
            var equipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();

            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            equipmentModelStateHourlyEarningsRepository.Setup(repo => repo.CreateAsync(equipmentModelStateHourlyEarnings))
            .ReturnsAsync(equipmentModelStateHourlyEarnings);


            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(equipmentModelStateHourlyEarningsRepository.Object);

            var result = await equipmentModelStateHourlyEarningService.CreateAsync(equipmentModelStateHourlyEarnings);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarnings>(result);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentModelStateHourlyEarnings.Value, result.Value);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentState, result.EquipmentState);

            equipmentModelStateHourlyEarningsRepository.Verify(repo => repo.CreateAsync(equipmentModelStateHourlyEarnings), Times.Once);
        }
    }
}

using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentStateS_Tests
{
    public class CreateAsync
    {
        [Fact]
        public async void CreateAsync_ValidEquipmentState_ReturnsCreatedEquipmentState()
        {
            var equipmentStateRepository = new Mock<IEquipmentStateR>();

            var equipmentState = new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            };

            equipmentStateRepository.Setup(repo => repo.CreateAsync(equipmentState))
            .ReturnsAsync(equipmentState);


            var equipmentStateService = new EquipmentStateS(equipmentStateRepository.Object);

            var result = await equipmentStateService.CreateAsync(equipmentState);

            Assert.NotNull(result);
            Assert.IsType<EquipmentState>(result);
            Assert.Equal(equipmentState.StateId, result.StateId);
            Assert.Equal(equipmentState.Color, result.Color);
            Assert.Equal(equipmentState.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);
            Assert.Equal(equipmentState.EquipmentStateHistories, result.EquipmentStateHistories);

            equipmentStateRepository.Verify(repo => repo.CreateAsync(equipmentState), Times.Once);
        }
    }
}

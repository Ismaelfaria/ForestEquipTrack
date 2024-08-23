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
    public class GetByIdAsync
    {
        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentState()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var validId = Guid.NewGuid();
            var equipmentState = new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            };

            mockEquipmentStateRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentState);

            var equipmentStateService = new EquipmentStateS(mockEquipmentStateRepository.Object);

            var result = await equipmentStateService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentState>(result);
            Assert.Equal(equipmentState.StateId, result.StateId);
            Assert.Equal(equipmentState.Color, result.Color);
            Assert.Equal(equipmentState.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);
            Assert.Equal(equipmentState.EquipmentStateHistories, result.EquipmentStateHistories);

            mockEquipmentStateRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var invalidId = Guid.Empty;

            var equipmentStateService = new EquipmentStateS(mockEquipmentStateRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

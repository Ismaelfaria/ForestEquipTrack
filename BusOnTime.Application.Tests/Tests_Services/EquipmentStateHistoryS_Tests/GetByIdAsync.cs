using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentStateHistoryS_Tests
{
    public class GetByIdAsync
    {
        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentStateHistory()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var validId = Guid.NewGuid();
            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentStateHistoryRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentStateHistory);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(mockEquipmentStateHistoryRepository.Object);

            var result = await equipmentStateHistoryService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistory>(result);
            Assert.Equal(equipmentStateHistory.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistory.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistory.Date, result.Date);
            Assert.Equal(equipmentStateHistory.Equipment, result.Equipment);
            Assert.Equal(equipmentStateHistory.EquipmentState, result.EquipmentState);

            mockEquipmentStateHistoryRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var invalidId = Guid.Empty;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(mockEquipmentStateHistoryRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateHistoryService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

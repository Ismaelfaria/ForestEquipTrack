using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentPositionHistoryS_Tests
{
    public class GetByIdAsync
    {
        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentPositionHistory()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var validId = Guid.NewGuid();
            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = validId,
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
                Equipment = new Equipment()
            };

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentPositionHistory);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(mockEquipmentPositionHistoryRepository.Object);

            var result = await equipmentPositionHistoryService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentPositionHistory>(result);
            Assert.Equal(equipmentPositionHistory.EquipmentPositionId, result.EquipmentPositionId);
            Assert.Equal(equipmentPositionHistory.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentPositionHistory.Date, result.Date);
            Assert.Equal(equipmentPositionHistory.Lon, result.Lon);
            Assert.Equal(equipmentPositionHistory.Lat, result.Lat);
            Assert.Equal(equipmentPositionHistory.Equipment, result.Equipment);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var invalidId = Guid.Empty;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(mockEquipmentPositionHistoryRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentPositionHistoryService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

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
    public class UpdateAsync
    {
        [Fact]
        public async Task UpdateAsync_ValidEquipmentPositionHistory_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
                Equipment = new Equipment()
            };

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.UpdateAsync(equipmentPositionHistory))
                .Returns(Task.CompletedTask);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(mockEquipmentPositionHistoryRepository.Object);

            await equipmentPositionHistoryService.UpdateAsync(equipmentPositionHistory);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.UpdateAsync(equipmentPositionHistory), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentPositionHistory_ThrowsArgumentNullException()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            EquipmentPositionHistory? nullEquipmentPositionHistory = null;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(mockEquipmentPositionHistoryRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentPositionHistoryService.UpdateAsync(nullEquipmentPositionHistory));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

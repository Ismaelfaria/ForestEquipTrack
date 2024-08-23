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
    public class UpdateAsync
    {
        [Fact]
        public async Task UpdateAsync_ValidEquipmentStateHistory_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentStateHistoryRepository.Setup(repo => repo.UpdateAsync(equipmentStateHistory))
                .Returns(Task.CompletedTask);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(mockEquipmentStateHistoryRepository.Object);

            await equipmentStateHistoryService.UpdateAsync(equipmentStateHistory);

            mockEquipmentStateHistoryRepository.Verify(repo => repo.UpdateAsync(equipmentStateHistory), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentStateHistory_ThrowsArgumentNullException()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            EquipmentStateHistory? nullEquipmentStateHistory = null;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(mockEquipmentStateHistoryRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentStateHistoryService.UpdateAsync(nullEquipmentStateHistory));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

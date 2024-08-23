using BusOnTime.Application.Services;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentStateHistoryS_Tests
{
    public class DeleteAsync
    {
        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var validId = Guid.NewGuid();

            mockEquipmentStateHistoryRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(mockEquipmentStateHistoryRepository.Object);

            await equipmentStateHistoryService.DeleteAsync(validId);

            mockEquipmentStateHistoryRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var invalidId = Guid.Empty;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(mockEquipmentStateHistoryRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateHistoryService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

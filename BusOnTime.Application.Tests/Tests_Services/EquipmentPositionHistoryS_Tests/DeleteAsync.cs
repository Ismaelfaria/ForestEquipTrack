using BusOnTime.Application.Services;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentPositionHistoryS_Tests
{
    public class DeleteAsync
    {
        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var validId = Guid.NewGuid();

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(mockEquipmentPositionHistoryRepository.Object);

            await equipmentPositionHistoryService.DeleteAsync(validId);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var invalidId = Guid.Empty;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(mockEquipmentPositionHistoryRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentPositionHistoryService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

using BusOnTime.Application.Services;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentStateS_Tests
{
    public class DeleteAsync
    {
        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var validId = Guid.NewGuid();

            mockEquipmentStateRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentStateService = new EquipmentStateS(mockEquipmentStateRepository.Object);

            await equipmentStateService.DeleteAsync(validId);

            mockEquipmentStateRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var invalidId = Guid.Empty;

            var equipmentStateService = new EquipmentStateS(mockEquipmentStateRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

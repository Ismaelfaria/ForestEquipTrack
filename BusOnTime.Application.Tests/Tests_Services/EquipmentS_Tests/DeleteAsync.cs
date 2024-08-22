using BusOnTime.Application.Services;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentS_Tests
{
    public class DeleteAsync
    {
        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var validId = Guid.NewGuid();

            mockEquipmentRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            await equipmentService.DeleteAsync(validId);

            mockEquipmentRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var invalidId = Guid.Empty;

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

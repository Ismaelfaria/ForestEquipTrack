using BusOnTime.Application.Services;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentModelS_Tests
{
    public class DeleteAsync
    {
        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var validId = Guid.NewGuid();

            mockEquipmentModelRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object);

            await equipmentModelService.DeleteAsync(validId);

            mockEquipmentModelRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var invalidId = Guid.Empty;

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

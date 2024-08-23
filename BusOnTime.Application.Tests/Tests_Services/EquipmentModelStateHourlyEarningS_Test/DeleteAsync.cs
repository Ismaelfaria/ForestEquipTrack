using BusOnTime.Application.Services;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentModelStateHourlyEarningS_Test
{
    public class DeleteAsync
    {
        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validId = Guid.NewGuid();

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            await equipmentModelStateHourlyEarningService.DeleteAsync(validId);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var invalidId = Guid.Empty;

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelStateHourlyEarningService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentModelStateHourlyEarningS_Test
{
    public class UpdateAsync
    {
        [Fact]
        public async Task UpdateAsync_ValidEquipmentModel_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings))
                .Returns(Task.CompletedTask);

            var equipmentModelStateHourlyEarningsService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            await equipmentModelStateHourlyEarningsService.UpdateAsync(equipmentModelStateHourlyEarnings);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentModel_ThrowsArgumentNullException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            EquipmentModelStateHourlyEarnings? nullEquipmentModelStateHourlyEarnings = null;

            var equipmentModelStateHourlyEarningsService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentModelStateHourlyEarningsService.UpdateAsync(nullEquipmentModelStateHourlyEarnings));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

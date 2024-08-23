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
    public class GetByIdAsync
    {
        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentModelStateHourlyEarnings()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validId = Guid.NewGuid();
            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentModelStateHourlyEarnings);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var result = await equipmentModelStateHourlyEarningService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarnings>(result);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentModelStateHourlyEarnings.Value, result.Value);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentState, result.EquipmentState);


            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var invalidId = Guid.Empty;

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelStateHourlyEarningService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

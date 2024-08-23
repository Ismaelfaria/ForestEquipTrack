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
    public class FindAllAsync
    {
        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentModelStateHourlyEarnings()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var equipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>
            {
            new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            },
            new EquipmentModelStateHourlyEarnings
            {
               EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 11,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            }
        };

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.FindAllAsync())
            .ReturnsAsync(equipmentModelStateHourlyEarnings);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var result = await equipmentModelStateHourlyEarningService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModelStateHourlyEarnings>>(result);
            Assert.Equal(equipmentModelStateHourlyEarnings.Count, result.Count());
            Assert.Contains(result, em => em.Value == 10);
            Assert.Contains(result, em => em.Value == 11);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }
    }
}

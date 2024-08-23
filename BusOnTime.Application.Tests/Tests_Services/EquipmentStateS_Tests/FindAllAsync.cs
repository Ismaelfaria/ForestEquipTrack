using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentStateS_Tests
{
    public class FindAllAsync
    {
        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentStates()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var equipmentStates = new List<EquipmentState>
            {
            new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            },
            new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Bulldozer",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            }
        };

            mockEquipmentStateRepository.Setup(repo => repo.FindAllAsync())
            .ReturnsAsync(equipmentStates);

            var equipmentStateService = new EquipmentStateS(mockEquipmentStateRepository.Object);

            var result = await equipmentStateService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentState>>(result);
            Assert.Equal(equipmentStates.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Excavator");
            Assert.Contains(result, em => em.Name == "Bulldozer");

            mockEquipmentStateRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }
    }
}

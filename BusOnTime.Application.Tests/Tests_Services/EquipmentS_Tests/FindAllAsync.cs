using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentS_Tests
{
    public class FindAllAsync
    {
        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipments()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var equipments = new List<Equipment>
            {
            new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Excavator",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            },
            new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Bulldozer",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            }
        };

            mockEquipmentRepository.Setup(repo => repo.FindAllAsync())
            .ReturnsAsync(equipments);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            var result = await equipmentService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<Equipment>>(result);
            Assert.Equal(equipments.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Excavator");
            Assert.Contains(result, em => em.Name == "Bulldozer");

            mockEquipmentRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }
    }
}

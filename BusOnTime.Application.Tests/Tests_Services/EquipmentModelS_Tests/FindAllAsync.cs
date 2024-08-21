using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentModelS_Tests
{
    public class FindAllAsync
    {
        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentModels()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var equipmentModels = new List<EquipmentModel>
            {
            new EquipmentModel
            {
                ModelId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator",
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            },
            new EquipmentModel
            {
                ModelId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Name = "Bulldozer",
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            }
        };

            mockEquipmentModelRepository.Setup(repo => repo.FindAllAsync())
            .ReturnsAsync(equipmentModels);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object);

            var result = await equipmentModelService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModel>>(result);
            Assert.Equal(equipmentModels.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Excavator");
            Assert.Contains(result, em => em.Name == "Bulldozer");

            mockEquipmentModelRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }
    }
}

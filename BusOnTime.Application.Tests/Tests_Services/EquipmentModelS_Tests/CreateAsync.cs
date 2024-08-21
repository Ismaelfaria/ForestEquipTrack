using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using BusOnTime.Data.Repositories.Concrete;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentModelS_Tests
{
    public class CreateAsync
    {
        [Fact]
        public async void CreateAsync_ValidEquipmentModel_ReturnsCreatedEquipmentModel()
        {
            var equipmentModelRepository = new Mock<IEquipmentModelR>();

            var equipmentModelEntity = new EquipmentModel
            {
                ModelId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator",
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            equipmentModelRepository.Setup(repo => repo.CreateAsync(equipmentModelEntity))
            .ReturnsAsync(equipmentModelEntity);


            var equipmentModelService = new EquipmentModelS(equipmentModelRepository.Object);

            var result = await equipmentModelService.CreateAsync(equipmentModelEntity);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModel>(result);
            Assert.Equal(equipmentModelEntity.ModelId, result.ModelId);
            Assert.Equal(equipmentModelEntity.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModelEntity.Name, result.Name);
            Assert.Equal(equipmentModelEntity.Equipment, result.Equipment);
            Assert.Equal(equipmentModelEntity.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);

            equipmentModelRepository.Verify(repo => repo.CreateAsync(equipmentModelEntity), Times.Once);
        }
    }
}

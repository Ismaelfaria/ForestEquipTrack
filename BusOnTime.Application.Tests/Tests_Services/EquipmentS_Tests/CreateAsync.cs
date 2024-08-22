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
    public class CreateAsync
    {
        [Fact]
        public async void CreateAsync_ValidEquipment_ReturnsCreatedEquipment()
        {
            var equipmentRepository = new Mock<IEquipmentR>();

            var equipmentEntity = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Excavator",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            equipmentRepository.Setup(repo => repo.CreateAsync(equipmentEntity))
            .ReturnsAsync(equipmentEntity);


            var equipmentService = new EquipmentS(equipmentRepository.Object);

            var result = await equipmentService.CreateAsync(equipmentEntity);

            Assert.NotNull(result);
            Assert.IsType<Equipment>(result);
            Assert.Equal(equipmentEntity.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentEntity.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentEntity.Name, result.Name);
            Assert.Equal(equipmentEntity.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipmentEntity.EquipmentStateHistories, result.EquipmentStateHistories);
            Assert.Equal(equipmentEntity.EquipmentPositionHistories, result.EquipmentPositionHistories);

            equipmentRepository.Verify(repo => repo.CreateAsync(equipmentEntity), Times.Once);
        }
    }
}

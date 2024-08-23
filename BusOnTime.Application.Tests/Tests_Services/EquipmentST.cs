using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services
{
    public class EquipmentST
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

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var validId = Guid.NewGuid();

            mockEquipmentRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            await equipmentService.DeleteAsync(validId);

            mockEquipmentRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var invalidId = Guid.Empty;

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

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

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipment()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var validId = Guid.NewGuid();
            var equipment = new Equipment
            {
                EquipmentId = validId,
                EquipmentModelId = Guid.NewGuid(),
                Name = "Bulldozer",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            mockEquipmentRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipment);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            var result = await equipmentService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<Equipment>(result);
            Assert.Equal(equipment.EquipmentId, result.EquipmentId);
            Assert.Equal(equipment.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipment.Name, result.Name);
            Assert.Equal(equipment.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipment.EquipmentStateHistories, result.EquipmentStateHistories);
            Assert.Equal(equipment.EquipmentPositionHistories, result.EquipmentPositionHistories);

            mockEquipmentRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var invalidId = Guid.Empty;

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipment_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var equipment = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Excavator",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            mockEquipmentRepository.Setup(repo => repo.UpdateAsync(equipment))
                .Returns(Task.CompletedTask);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            await equipmentService.UpdateAsync(equipment);

            mockEquipmentRepository.Verify(repo => repo.UpdateAsync(equipment), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipment_ThrowsArgumentNullException()
        {
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            Equipment? nullEquipment = null;

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentService.UpdateAsync(nullEquipment));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

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
    public class GetByIdAsync
    {
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
    }
}

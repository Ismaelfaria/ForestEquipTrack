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
    public class GetByIdAsync
    {
        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentModel()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var validId = Guid.NewGuid();
            var equipmentModel = new EquipmentModel
            {
                ModelId = validId,
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator",
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            mockEquipmentModelRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentModel);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object);

            var result = await equipmentModelService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModel>(result);
            Assert.Equal(equipmentModel.ModelId, result.ModelId);
            Assert.Equal(equipmentModel.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModel.Name, result.Name);
            Assert.Equal(equipmentModel.Equipment, result.Equipment);
            Assert.Equal(equipmentModel.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);

            mockEquipmentModelRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var invalidId = Guid.Empty;

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }
    }
}

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
    public class UpdateAsync
    {
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

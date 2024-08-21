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
    public class UpdateAsync
    {
        [Fact]
        public async Task UpdateAsync_ValidEquipmentModel_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var equipmentModel = new EquipmentModel
            {
                ModelId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator",
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            mockEquipmentModelRepository.Setup(repo => repo.UpdateAsync(equipmentModel))
                .Returns(Task.CompletedTask);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object);

            await equipmentModelService.UpdateAsync(equipmentModel);

            mockEquipmentModelRepository.Verify(repo => repo.UpdateAsync(equipmentModel), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentModel_ThrowsArgumentNullException()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            EquipmentModel? nullEquipmentModel = null;

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentModelService.UpdateAsync(nullEquipmentModel));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

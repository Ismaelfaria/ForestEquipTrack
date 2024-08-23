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
    public class UpdateAsync
    {
        [Fact]
        public async Task UpdateAsync_ValidEquipmentState_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var equipmentState = new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            };

            mockEquipmentStateRepository.Setup(repo => repo.UpdateAsync(equipmentState))
                .Returns(Task.CompletedTask);

            var equipmentStateService = new EquipmentStateS(mockEquipmentStateRepository.Object);

            await equipmentStateService.UpdateAsync(equipmentState);

            mockEquipmentStateRepository.Verify(repo => repo.UpdateAsync(equipmentState), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentState_ThrowsArgumentNullException()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            EquipmentState? nullEquipmentState = null;

            var equipmentStateService = new EquipmentStateS(mockEquipmentStateRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentStateService.UpdateAsync(nullEquipmentState));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

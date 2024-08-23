using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentStateHistoryS_Tests
{
    public class CreateAsync
    {
        [Fact]
        public async void CreateAsync_ValidEquipmentStateHistory_ReturnsCreatedEquipmentStateHistory()
        {
            var equipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();

            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            equipmentStateHistoryRepository.Setup(repo => repo.CreateAsync(equipmentStateHistory))
            .ReturnsAsync(equipmentStateHistory);


            var equipmentStateHistoryService = new EquipmentStateHistoryS(equipmentStateHistoryRepository.Object);

            var result = await equipmentStateHistoryService.CreateAsync(equipmentStateHistory);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistory>(result);
            Assert.Equal(equipmentStateHistory.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistory.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistory.Date, result.Date);
            Assert.Equal(equipmentStateHistory.Equipment, result.Equipment);
            Assert.Equal(equipmentStateHistory.EquipmentState, result.EquipmentState);

            equipmentStateHistoryRepository.Verify(repo => repo.CreateAsync(equipmentStateHistory), Times.Once);
        }
    }
}

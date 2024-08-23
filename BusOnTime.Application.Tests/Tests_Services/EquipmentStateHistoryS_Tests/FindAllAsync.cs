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
    public class FindAllAsync
    {
        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentStateHistorys()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var equipmentStateHistorys = new List<EquipmentStateHistory>
            {
            new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            },
            new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            }
        };

            mockEquipmentStateHistoryRepository.Setup(repo => repo.FindAllAsync())
            .ReturnsAsync(equipmentStateHistorys);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(mockEquipmentStateHistoryRepository.Object);

            var result = await equipmentStateHistoryService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentStateHistory>>(result);
            Assert.Equal(equipmentStateHistorys.Count, result.Count());

            mockEquipmentStateHistoryRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }
    }
}

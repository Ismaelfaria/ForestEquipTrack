using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services.EquipmentPositionHistoryS_Tests
{
    public class FindAllAsync
    {
        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentPositionHistorys()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var equipmentPositionHistorys = new List<EquipmentPositionHistory>
            {
            new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
                Equipment = new Equipment()
            },
            new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 3,
                Lon = 4,
                Equipment = new Equipment()
            }
        };

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.FindAllAsync())
            .ReturnsAsync(equipmentPositionHistorys);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(mockEquipmentPositionHistoryRepository.Object);

            var result = await equipmentPositionHistoryService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentPositionHistory>>(result);
            Assert.Equal(equipmentPositionHistorys.Count, result.Count());
            Assert.Contains(result, em => em.Lat == 1);
            Assert.Contains(result, em => em.Lat == 3);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }
    }
}

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
    public class CreateAsync
    {
        [Fact]
        public async void CreateAsync_ValidEquipmentPositionHistory_ReturnsCreatedEquipmentPositionHistory()
        {
            var equipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();

            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
                Equipment = new Equipment()
            };

            equipmentPositionHistoryRepository.Setup(repo => repo.CreateAsync(equipmentPositionHistory))
            .ReturnsAsync(equipmentPositionHistory);


            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(equipmentPositionHistoryRepository.Object);

            var result = await equipmentPositionHistoryService.CreateAsync(equipmentPositionHistory);

            Assert.NotNull(result);
            Assert.IsType<EquipmentPositionHistory>(result);
            Assert.Equal(equipmentPositionHistory.EquipmentPositionId, result.EquipmentPositionId);
            Assert.Equal(equipmentPositionHistory.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentPositionHistory.Date, result.Date);
            Assert.Equal(equipmentPositionHistory.Lon, result.Lon);
            Assert.Equal(equipmentPositionHistory.Lat, result.Lat);
            Assert.Equal(equipmentPositionHistory.Equipment, result.Equipment);

            equipmentPositionHistoryRepository.Verify(repo => repo.CreateAsync(equipmentPositionHistory), Times.Once);
        }
    }
}

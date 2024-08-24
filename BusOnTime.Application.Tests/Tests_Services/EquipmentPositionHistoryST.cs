using AutoMapper;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services
{
    public class EquipmentPositionHistoryST
    {
        [Fact]
        public async Task CreateAsync_ValidEquipmentPositionHistory_ReturnsCreatedEquipmentPositionHistory()
        {
            var equipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();

            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
                Equipment = new Equipment()
            };

            var equipmentPositionHistoryIM = new EquipmentPositionHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
            };

            validator.Setup(v => v.Validate(It.IsAny<EquipmentPositionHistoryIM>()))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mockMapper.Setup(m => m.Map<EquipmentPositionHistory>(It.IsAny<EquipmentPositionHistoryIM>()))
                      .Returns(equipmentPositionHistory);

            equipmentPositionHistoryRepository.Setup(repo => repo.CreateAsync(equipmentPositionHistory))
                .ReturnsAsync(equipmentPositionHistory);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                equipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            var result = await equipmentPositionHistoryService.CreateAsync(equipmentPositionHistoryIM);

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

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var validId = Guid.NewGuid();

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            await equipmentPositionHistoryService.DeleteAsync(validId);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentPositionHistoryService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentPositionHistories()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var equipmentPositionHistories = new List<EquipmentPositionHistory>
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
                .ReturnsAsync(equipmentPositionHistories);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            var result = await equipmentPositionHistoryService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentPositionHistory>>(result);
            Assert.Equal(equipmentPositionHistories.Count, result.Count());
            Assert.Contains(result, ep => ep.Lat == 1);
            Assert.Contains(result, ep => ep.Lat == 3);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentPositionHistory()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var validId = Guid.NewGuid();
            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = validId,
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
                Equipment = new Equipment()
            };

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentPositionHistory);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            var result = await equipmentPositionHistoryService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentPositionHistory>(result);
            Assert.Equal(equipmentPositionHistory.EquipmentPositionId, result.EquipmentPositionId);
            Assert.Equal(equipmentPositionHistory.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentPositionHistory.Date, result.Date);
            Assert.Equal(equipmentPositionHistory.Lon, result.Lon);
            Assert.Equal(equipmentPositionHistory.Lat, result.Lat);
            Assert.Equal(equipmentPositionHistory.Equipment, result.Equipment);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentPositionHistoryService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentPositionHistory_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
                Equipment = new Equipment()
            };

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.UpdateAsync(equipmentPositionHistory))
                .Returns(Task.CompletedTask);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            await equipmentPositionHistoryService.UpdateAsync(equipmentPositionHistory);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.UpdateAsync(equipmentPositionHistory), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentPositionHistory_ThrowsArgumentNullException()
        {
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            EquipmentPositionHistory? nullEquipmentPositionHistory = null;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentPositionHistoryService.UpdateAsync(nullEquipmentPositionHistory));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

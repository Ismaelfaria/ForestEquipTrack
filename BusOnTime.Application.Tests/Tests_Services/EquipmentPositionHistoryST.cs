using AutoMapper;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
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
            // Arrange
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

            var equipmentPositionHistoryVM = new EquipmentPositionHistoryVM
            {
                EquipmentPositionId = equipmentPositionHistory.EquipmentPositionId,
                EquipmentId = equipmentPositionHistory.EquipmentId,
                Date = equipmentPositionHistory.Date,
                Lat = equipmentPositionHistory.Lat,
                Lon = equipmentPositionHistory.Lon
            };

            validator.Setup(v => v.Validate(It.IsAny<EquipmentPositionHistoryIM>()))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mockMapper.Setup(m => m.Map<EquipmentPositionHistory>(It.IsAny<EquipmentPositionHistoryIM>()))
                      .Returns(equipmentPositionHistory);

            mockMapper.Setup(m => m.Map<EquipmentPositionHistoryVM>(It.IsAny<EquipmentPositionHistory>()))
                      .Returns(equipmentPositionHistoryVM);

            equipmentPositionHistoryRepository.Setup(repo => repo.CreateAsync(It.IsAny<EquipmentPositionHistory>()))
                .ReturnsAsync(equipmentPositionHistory);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                equipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentPositionHistoryService.CreateAsync(equipmentPositionHistoryIM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentPositionHistoryVM>(result);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentPositionId, result.EquipmentPositionId);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentPositionHistoryVM.Date, result.Date);
            Assert.Equal(equipmentPositionHistoryVM.Lon, result.Lon);
            Assert.Equal(equipmentPositionHistoryVM.Lat, result.Lat);

            equipmentPositionHistoryRepository.Verify(repo => repo.CreateAsync(It.IsAny<EquipmentPositionHistory>()), Times.Once);
            validator.Verify(v => v.Validate(It.IsAny<EquipmentPositionHistoryIM>()), Times.Once);
            mockMapper.Verify(m => m.Map<EquipmentPositionHistory>(It.IsAny<EquipmentPositionHistoryIM>()), Times.Once);
            mockMapper.Verify(m => m.Map<EquipmentPositionHistoryVM>(It.IsAny<EquipmentPositionHistory>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            // Arrange
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

            // Act
            await equipmentPositionHistoryService.DeleteAsync(validId);

            // Assert
            mockEquipmentPositionHistoryRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentPositionHistoryService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentPositionHistories()
        {
            // Arrange
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

            var equipmentPositionHistoryVMs = equipmentPositionHistories.Select(ep => new EquipmentPositionHistoryVM
            {
                EquipmentPositionId = ep.EquipmentPositionId,
                EquipmentId = ep.EquipmentId,
                Date = ep.Date,
                Lat = ep.Lat,
                Lon = ep.Lon
            });

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentPositionHistories);

            mockMapper.Setup(m => m.Map<IEnumerable<EquipmentPositionHistoryVM>>(equipmentPositionHistories))
                      .Returns(equipmentPositionHistoryVMs);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentPositionHistoryService.FindAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<EquipmentPositionHistoryVM>>(result.ToList());
            Assert.Equal(equipmentPositionHistoryVMs.Count(), result.Count());

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
            mockMapper.Verify(m => m.Map<IEnumerable<EquipmentPositionHistoryVM>>(equipmentPositionHistories), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentPositionHistory()
        {
            // Arrange
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

            var equipmentPositionHistoryVM = new EquipmentPositionHistoryVM
            {
                EquipmentPositionId = equipmentPositionHistory.EquipmentPositionId,
                EquipmentId = equipmentPositionHistory.EquipmentId,
                Date = equipmentPositionHistory.Date,
                Lat = equipmentPositionHistory.Lat,
                Lon = equipmentPositionHistory.Lon
            };

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentPositionHistory);

            mockMapper.Setup(m => m.Map<EquipmentPositionHistoryVM>(equipmentPositionHistory))
                      .Returns(equipmentPositionHistoryVM);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentPositionHistoryService.GetByIdAsync(validId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentPositionHistoryVM>(result);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentPositionId, result.EquipmentPositionId);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentPositionHistoryVM.Date, result.Date);
            Assert.Equal(equipmentPositionHistoryVM.Lon, result.Lon);
            Assert.Equal(equipmentPositionHistoryVM.Lat, result.Lat);

            mockEquipmentPositionHistoryRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            mockMapper.Verify(m => m.Map<EquipmentPositionHistoryVM>(equipmentPositionHistory), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentPositionHistoryService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentPositionHistory_CallsUpdateAsyncInRepository()
        {
            // Arrange
            var mockEquipmentPositionHistoryRepository = new Mock<IEquipmentPositionHistoryR>();
            var mockMapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentPositionHistoryIM>>();

            var equipmentPositionHistoryIM = new EquipmentPositionHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1,
                Lon = 2,
            };

            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = equipmentPositionHistoryIM.EquipmentId,
                Date = equipmentPositionHistoryIM.Date,
                Lat = equipmentPositionHistoryIM.Lat,
                Lon = equipmentPositionHistoryIM.Lon,
                Equipment = new Equipment()
            };

            validator.Setup(v => v.Validate(It.IsAny<EquipmentPositionHistoryIM>()))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mockMapper.Setup(m => m.Map<EquipmentPositionHistory>(equipmentPositionHistoryIM))
                      .Returns(equipmentPositionHistory);

            mockEquipmentPositionHistoryRepository.Setup(repo => repo.UpdateAsync(It.IsAny<EquipmentPositionHistory>()))
                .Returns(Task.CompletedTask);

            var equipmentPositionHistoryService = new EquipmentPositionHistoryS(
                mockEquipmentPositionHistoryRepository.Object,
                mockMapper.Object,
                validator.Object
            );

            // Act
            await equipmentPositionHistoryService.UpdateAsync(equipmentPositionHistory.EquipmentPositionId, equipmentPositionHistoryIM);

            // Assert
            mockEquipmentPositionHistoryRepository.Verify(repo => repo.UpdateAsync(It.IsAny<EquipmentPositionHistory>()), Times.Once);
            validator.Verify(v => v.Validate(It.IsAny<EquipmentPositionHistoryIM>()), Times.Once);
            mockMapper.Verify(m => m.Map<EquipmentPositionHistory>(It.IsAny<EquipmentPositionHistoryIM>()), Times.Once);
        }
    }
}

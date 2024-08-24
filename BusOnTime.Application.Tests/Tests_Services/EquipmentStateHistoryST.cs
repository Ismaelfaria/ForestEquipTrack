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
    public class EquipmentStateHistoryST
    {
        [Fact]
        public async Task CreateAsync_ValidEquipmentStateHistory_ReturnsCreatedEquipmentStateHistory()
        {
            var equipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();

            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            var equipmentStateHistoryIM = new EquipmentStateHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            validator.Setup(v => v.Validate(It.IsAny<EquipmentStateHistoryIM>()))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mapper.Setup(m => m.Map<EquipmentStateHistory>(It.IsAny<EquipmentStateHistoryIM>()))
                  .Returns(equipmentStateHistory);

            equipmentStateHistoryRepository.Setup(repo => repo.CreateAsync(equipmentStateHistory))
                                           .ReturnsAsync(equipmentStateHistory);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                equipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            var result = await equipmentStateHistoryService.CreateAsync(equipmentStateHistoryIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistory>(result);
            Assert.Equal(equipmentStateHistory.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistory.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistory.Date, result.Date);
            Assert.Equal(equipmentStateHistory.Equipment, result.Equipment);
            Assert.Equal(equipmentStateHistory.EquipmentState, result.EquipmentState);

            equipmentStateHistoryRepository.Verify(repo => repo.CreateAsync(equipmentStateHistory), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var validId = Guid.NewGuid();

            mockEquipmentStateHistoryRepository.Setup(repo => repo.DeleteAsync(validId))
                                               .Returns(Task.CompletedTask);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            await equipmentStateHistoryService.DeleteAsync(validId);

            mockEquipmentStateHistoryRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateHistoryService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentStateHistories()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();

            var equipmentStateHistories = new List<EquipmentStateHistory>
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
                                               .ReturnsAsync(equipmentStateHistories);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            var result = await equipmentStateHistoryService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentStateHistory>>(result);
            Assert.Equal(equipmentStateHistories.Count, result.Count());

            mockEquipmentStateHistoryRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentStateHistory()
        {
            // Arrange
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var validId = Guid.NewGuid();
            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentStateHistoryRepository.Setup(repo => repo.GetByIdAsync(validId))
                                               .ReturnsAsync(equipmentStateHistory);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            var result = await equipmentStateHistoryService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistory>(result);
            Assert.Equal(equipmentStateHistory.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistory.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistory.Date, result.Date);
            Assert.Equal(equipmentStateHistory.Equipment, result.Equipment);
            Assert.Equal(equipmentStateHistory.EquipmentState, result.EquipmentState);

            mockEquipmentStateHistoryRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateHistoryService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentStateHistory_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentStateHistoryRepository.Setup(repo => repo.UpdateAsync(equipmentStateHistory))
                                               .Returns(Task.CompletedTask);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            await equipmentStateHistoryService.UpdateAsync(equipmentStateHistory);

            mockEquipmentStateHistoryRepository.Verify(repo => repo.UpdateAsync(equipmentStateHistory), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentStateHistory_ThrowsArgumentNullException()
        {
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            EquipmentStateHistory? nullEquipmentStateHistory = null;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentStateHistoryService.UpdateAsync(nullEquipmentStateHistory));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

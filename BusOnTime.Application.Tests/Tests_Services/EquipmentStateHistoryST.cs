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
    public class EquipmentStateHistoryST
    {
        [Fact]
        public async Task CreateAsync_ValidEquipmentStateHistory_ReturnsCreatedEquipmentStateHistory()
        {
            // Arrange
            var equipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();

            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            var equipmentStateHistoryIM = new EquipmentStateHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            var equipmentStateHistoryVM = new EquipmentStateHistoryVM
            {
                EquipmentStateId = equipmentStateHistory.EquipmentStateId,
                EquipmentId = equipmentStateHistory.EquipmentId,
                Date = equipmentStateHistory.Date
            };

            validator.Setup(v => v.Validate(It.IsAny<EquipmentStateHistoryIM>()))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mapper.Setup(m => m.Map<EquipmentStateHistory>(It.IsAny<EquipmentStateHistoryIM>()))
                  .Returns(equipmentStateHistory);

            mapper.Setup(m => m.Map<EquipmentStateHistoryVM>(It.IsAny<EquipmentStateHistory>()))
                  .Returns(equipmentStateHistoryVM);

            equipmentStateHistoryRepository.Setup(repo => repo.CreateAsync(equipmentStateHistory))
                                           .ReturnsAsync(equipmentStateHistory);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                equipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentStateHistoryService.CreateAsync(equipmentStateHistoryIM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistoryVM>(result);
            Assert.Equal(equipmentStateHistoryVM.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistoryVM.Date, result.Date);

            equipmentStateHistoryRepository.Verify(repo => repo.CreateAsync(equipmentStateHistory), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            // Arrange
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

            // Act
            await equipmentStateHistoryService.DeleteAsync(validId);

            // Assert
            mockEquipmentStateHistoryRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateHistoryService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentStateHistories()
        {
            // Arrange
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();

            var equipmentStateHistories = new List<EquipmentStateHistory>
            {
                new EquipmentStateHistory
                {
                    EquipmentStateId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = DateTime.Now
                },
                new EquipmentStateHistory
                {
                    EquipmentStateId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = DateTime.Now
                }
            };

            var equipmentStateHistoryVMs = equipmentStateHistories.Select(e => new EquipmentStateHistoryVM
            {
                EquipmentStateId = e.EquipmentStateId,
                EquipmentId = e.EquipmentId,
                Date = e.Date
            }).ToList();

            mockEquipmentStateHistoryRepository.Setup(repo => repo.FindAllAsync())
                                               .ReturnsAsync(equipmentStateHistories);

            mapper.Setup(m => m.Map<IEnumerable<EquipmentStateHistoryVM>>(It.IsAny<IEnumerable<EquipmentStateHistory>>()))
                  .Returns(equipmentStateHistoryVMs);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentStateHistoryService.FindAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<EquipmentStateHistoryVM>>(result);
            Assert.Equal(equipmentStateHistoryVMs.Count, result.Count());

            // Verifica que todos os itens da lista retornada estão corretos
            foreach (var vm in equipmentStateHistoryVMs)
            {
                Assert.Contains(result, r => r.EquipmentStateId == vm.EquipmentStateId &&
                                              r.EquipmentId == vm.EquipmentId &&
                                              r.Date == vm.Date);
            }

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
                EquipmentStateId = validId,
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            var equipmentStateHistoryVM = new EquipmentStateHistoryVM
            {
                EquipmentStateId = validId,
                EquipmentId = equipmentStateHistory.EquipmentId,
                Date = equipmentStateHistory.Date
            };

            mockEquipmentStateHistoryRepository.Setup(repo => repo.GetByIdAsync(validId))
                                               .ReturnsAsync(equipmentStateHistory);

            mapper.Setup(m => m.Map<EquipmentStateHistoryVM>(It.IsAny<EquipmentStateHistory>()))
                  .Returns(equipmentStateHistoryVM);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentStateHistoryService.GetByIdAsync(validId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistoryVM>(result);
            Assert.Equal(equipmentStateHistoryVM.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistoryVM.Date, result.Date);

            mockEquipmentStateHistoryRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateHistoryService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentStateHistory_CallsUpdateAsyncInRepository()
        {
            // Arrange
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var validId = Guid.NewGuid();
            var equipmentStateHistoryIM = new EquipmentStateHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateId = validId,
                EquipmentId = equipmentStateHistoryIM.EquipmentId,
                Date = equipmentStateHistoryIM.Date
            };

            var equipmentStateHistoryVM = new EquipmentStateHistoryVM
            {
                EquipmentStateId = validId,
                EquipmentId = equipmentStateHistoryIM.EquipmentId,
                Date = equipmentStateHistoryIM.Date
            };

            validator.Setup(v => v.Validate(It.IsAny<EquipmentStateHistoryIM>()))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mapper.Setup(m => m.Map<EquipmentStateHistory>(It.IsAny<EquipmentStateHistoryIM>()))
                  .Returns(equipmentStateHistory);

            mapper.Setup(m => m.Map<EquipmentStateHistoryVM>(It.IsAny<EquipmentStateHistory>()))
                  .Returns(equipmentStateHistoryVM);

            mockEquipmentStateHistoryRepository.Setup(repo => repo.UpdateAsync(equipmentStateHistory))
                                               .Returns(Task.CompletedTask);

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            await equipmentStateHistoryService.UpdateAsync(validId, equipmentStateHistoryIM);

            // Assert
            mockEquipmentStateHistoryRepository.Verify(repo => repo.UpdateAsync(equipmentStateHistory), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentStateHistory_ThrowsArgumentNullException()
        {
            // Arrange
            var mockEquipmentStateHistoryRepository = new Mock<IEquipmentStateHistoryR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateHistoryIM>>();
            var validId = Guid.NewGuid();

            var equipmentStateHistoryService = new EquipmentStateHistoryS(
                mockEquipmentStateHistoryRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentStateHistoryService.UpdateAsync(validId, null));
            Assert.Equal("Value cannot be null. (Parameter 'equipmentStateHistoryIM')", exception.Message);
        }
    }
}

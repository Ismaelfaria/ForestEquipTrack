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
    public class EquipmentStateST
    {
        [Fact]
        public async Task CreateAsync_ValidEquipmentStateIM_ReturnsCreatedEquipmentState()
        {
            // Arrange
            var equipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();

            var equipmentState = new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            };

            var equipmentStateIM = new EquipmentStateIM
            {
                Color = "ColorTest",
                Name = "Excavator"
            };

            var equipmentStateVM = new EquipmentStateVM
            {
                StateId = equipmentState.StateId,
                Color = equipmentState.Color,
                Name = equipmentState.Name,
                EquipmentModelStateHourlyEarnings = equipmentState.EquipmentModelStateHourlyEarnings,
                EquipmentStateHistories = equipmentState.EquipmentStateHistories
            };

            validator.Setup(v => v.Validate(equipmentStateIM))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mapper.Setup(m => m.Map<EquipmentState>(equipmentStateIM))
                  .Returns(equipmentState);

            mapper.Setup(m => m.Map<EquipmentStateVM>(equipmentState))
                  .Returns(equipmentStateVM);

            equipmentStateRepository.Setup(repo => repo.CreateAsync(equipmentState))
                                    .ReturnsAsync(equipmentState);

            var equipmentStateService = new EquipmentStateS(
                equipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentStateService.CreateAsync(equipmentStateIM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentStateVM>(result);
            Assert.Equal(equipmentStateVM.StateId, result.StateId);
            Assert.Equal(equipmentStateVM.Color, result.Color);
            Assert.Equal(equipmentStateVM.Name, result.Name);
            Assert.Equal(equipmentStateVM.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);
            Assert.Equal(equipmentStateVM.EquipmentStateHistories, result.EquipmentStateHistories);

            equipmentStateRepository.Verify(repo => repo.CreateAsync(equipmentState), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            // Arrange
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var validId = Guid.NewGuid();

            mockEquipmentStateRepository.Setup(repo => repo.DeleteAsync(validId))
                                        .Returns(Task.CompletedTask);

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            await equipmentStateService.DeleteAsync(validId);

            // Assert
            mockEquipmentStateRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID: Guid.Empty", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentStates()
        {
            // Arrange
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();

            var equipmentStates = new List<EquipmentState>
            {
                new EquipmentState
                {
                    StateId = Guid.NewGuid(),
                    Color = "ColorTest",
                    Name = "Excavator",
                    EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                    EquipmentStateHistories = new List<EquipmentStateHistory>()
                },
                new EquipmentState
                {
                    StateId = Guid.NewGuid(),
                    Color = "ColorTest",
                    Name = "Bulldozer",
                    EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                    EquipmentStateHistories = new List<EquipmentStateHistory>()
                }
            };

            var equipmentStateVMs = equipmentStates.Select(es => new EquipmentStateVM
            {
                StateId = es.StateId,
                Color = es.Color,
                Name = es.Name,
                EquipmentModelStateHourlyEarnings = es.EquipmentModelStateHourlyEarnings,
                EquipmentStateHistories = es.EquipmentStateHistories
            }).ToList();

            mockEquipmentStateRepository.Setup(repo => repo.FindAllAsync())
                                        .ReturnsAsync(equipmentStates);

            mapper.Setup(m => m.Map<IEnumerable<EquipmentStateVM>>(equipmentStates))
                  .Returns(equipmentStateVMs);

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentStateService.FindAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<EquipmentStateVM>>(result);
            Assert.Equal(equipmentStateVMs.Count, result.Count());
            Assert.Contains(result, esvm => esvm.Name == "Excavator");
            Assert.Contains(result, esvm => esvm.Name == "Bulldozer");

            mockEquipmentStateRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentState()
        {
            // Arrange
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var validId = Guid.NewGuid();
            var equipmentState = new EquipmentState
            {
                StateId = validId,
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            };

            var equipmentStateVM = new EquipmentStateVM
            {
                StateId = equipmentState.StateId,
                Color = equipmentState.Color,
                Name = equipmentState.Name,
                EquipmentModelStateHourlyEarnings = equipmentState.EquipmentModelStateHourlyEarnings,
                EquipmentStateHistories = equipmentState.EquipmentStateHistories
            };

            mockEquipmentStateRepository.Setup(repo => repo.GetByIdAsync(validId))
                                        .ReturnsAsync(equipmentState);

            mapper.Setup(m => m.Map<EquipmentStateVM>(equipmentState))
                  .Returns(equipmentStateVM);

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            var result = await equipmentStateService.GetByIdAsync(validId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentStateVM>(result);
            Assert.Equal(equipmentStateVM.StateId, result.StateId);
            Assert.Equal(equipmentStateVM.Color, result.Color);
            Assert.Equal(equipmentStateVM.Name, result.Name);
            Assert.Equal(equipmentStateVM.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);
            Assert.Equal(equipmentStateVM.EquipmentStateHistories, result.EquipmentStateHistories);

            mockEquipmentStateRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID: Guid.Empty", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentState_CallsUpdateAsyncInRepository()
        {
            // Arrange
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var equipmentState = new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            };

            var equipmentStateIM = new EquipmentStateIM
            {
                Color = "ColorTest",
                Name = "Excavator"
            };

            validator.Setup(v => v.Validate(equipmentStateIM))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mapper.Setup(m => m.Map<EquipmentState>(equipmentStateIM))
                  .Returns(equipmentState);

            mockEquipmentStateRepository.Setup(repo => repo.UpdateAsync(equipmentState))
                                        .Returns(Task.CompletedTask);

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act
            await equipmentStateService.UpdateAsync(equipmentState.StateId,equipmentStateIM);

            // Assert
            mockEquipmentStateRepository.Verify(repo => repo.UpdateAsync(equipmentState), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentState_ThrowsArgumentNullException()
        {
            // Arrange
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            EquipmentStateIM nullEquipmentStateIM = null;

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentStateService.UpdateAsync(Guid.Empty,nullEquipmentStateIM));
            Assert.Equal("Value cannot be null. (Parameter 'equipmentStateIM')", exception.Message);
        }

    }
}

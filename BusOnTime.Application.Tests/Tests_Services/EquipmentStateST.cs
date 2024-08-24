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
    public class EquipmentStateST
    {
        [Fact]
        public async Task CreateAsync_ValidEquipmentStateIM_ReturnsCreatedEquipmentState()
        {
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

            validator.Setup(v => v.Validate(equipmentStateIM))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mapper.Setup(m => m.Map<EquipmentState>(equipmentStateIM))
                  .Returns(equipmentState);

            equipmentStateRepository.Setup(repo => repo.CreateAsync(equipmentState))
                                    .ReturnsAsync(equipmentState);

            var equipmentStateService = new EquipmentStateS(
                equipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            var result = await equipmentStateService.CreateAsync(equipmentStateIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentState>(result);
            Assert.Equal(equipmentState.StateId, result.StateId);
            Assert.Equal(equipmentState.Color, result.Color);
            Assert.Equal(equipmentState.Name, result.Name);
            Assert.Equal(equipmentState.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);
            Assert.Equal(equipmentState.EquipmentStateHistories, result.EquipmentStateHistories);

            equipmentStateRepository.Verify(repo => repo.CreateAsync(equipmentState), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
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

            await equipmentStateService.DeleteAsync(validId);

            mockEquipmentStateRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentStates()
        {
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

            mockEquipmentStateRepository.Setup(repo => repo.FindAllAsync())
                                        .ReturnsAsync(equipmentStates);

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            var result = await equipmentStateService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentState>>(result);
            Assert.Equal(equipmentStates.Count, result.Count());
            Assert.Contains(result, es => es.Name == "Excavator");
            Assert.Contains(result, es => es.Name == "Bulldozer");

            mockEquipmentStateRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentState()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var validId = Guid.NewGuid();
            var equipmentState = new EquipmentState
            {
                StateId = Guid.NewGuid(),
                Color = "ColorTest",
                Name = "Excavator",
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>(),
                EquipmentStateHistories = new List<EquipmentStateHistory>()
            };

            mockEquipmentStateRepository.Setup(repo => repo.GetByIdAsync(validId))
                                        .ReturnsAsync(equipmentState);

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            var result = await equipmentStateService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentState>(result);
            Assert.Equal(equipmentState.StateId, result.StateId);
            Assert.Equal(equipmentState.Color, result.Color);
            Assert.Equal(equipmentState.Name, result.Name);
            Assert.Equal(equipmentState.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);
            Assert.Equal(equipmentState.EquipmentStateHistories, result.EquipmentStateHistories);

            mockEquipmentStateRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            var invalidId = Guid.Empty;

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentStateService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentState_CallsUpdateAsyncInRepository()
        {
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

            mockEquipmentStateRepository.Setup(repo => repo.UpdateAsync(equipmentState))
                                        .Returns(Task.CompletedTask);

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            await equipmentStateService.UpdateAsync(equipmentState);

            mockEquipmentStateRepository.Verify(repo => repo.UpdateAsync(equipmentState), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentState_ThrowsArgumentNullException()
        {
            var mockEquipmentStateRepository = new Mock<IEquipmentStateR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentStateIM>>();
            EquipmentState? nullEquipmentState = null;

            var equipmentStateService = new EquipmentStateS(
                mockEquipmentStateRepository.Object,
                mapper.Object,
                validator.Object
            );

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentStateService.UpdateAsync(nullEquipmentState));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

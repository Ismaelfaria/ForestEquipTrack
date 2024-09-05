using AutoMapper;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Application.Services;
using BusOnTime.Domain.Entities;
using BusOnTime.Infrastructure.Interfaces.Interface;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Application.Tests.Tests_Services
{
    public class EquipmentStateST
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentStateIM>> _validatorMock;
        private readonly Mock<IEquipmentStateR> _equipmentRMock;
        private readonly EquipmentStateS _service;

        public EquipmentStateST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentStateIM>>();
            _equipmentRMock = new Mock<IEquipmentStateR>();

            _service = new EquipmentStateS(_equipmentRMock.Object, _mapperMock.Object, _validatorMock.Object);
        }


        [Fact]
        public async void CreateAsync_ValidEquipment_ReturnsCreatedEquipmentStateVM()
        {

            var equipmentStateIM = new EquipmentStateIM
            {
                Name = "TestName",
                Color = "Color"
            };

            var equipmentStateEntity = new EquipmentState
            {
                EquipmentStateId = Guid.NewGuid(),
                Name = equipmentStateIM.Name, 
                Color = equipmentStateIM.Color,
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()   
            };

            var equipmentStateVM = new EquipmentStateVM
            {
                Name = equipmentStateEntity.Name,
                Color = equipmentStateEntity.Color,
                EquipmentStateId = equipmentStateEntity.EquipmentStateId
            };


            _validatorMock.Setup(v => v.Validate(equipmentStateIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentState>(equipmentStateIM))
                .Returns(equipmentStateEntity);

            _mapperMock.Setup(m => m.Map<EquipmentStateVM>(equipmentStateEntity))
                .Returns(equipmentStateVM);

            _equipmentRMock.Setup(repo => repo.CreateAsync(equipmentStateEntity))
                .ReturnsAsync(equipmentStateEntity);

            var result = await _service.CreateAsync(equipmentStateIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateVM>(result);
            Assert.Equal(equipmentStateVM.Name, result.Name);
            Assert.Equal(equipmentStateVM.Color, result.Color);
            Assert.Equal(equipmentStateVM.EquipmentStateId, result.EquipmentStateId);

            _equipmentRMock.Verify(repo => repo.CreateAsync(equipmentStateEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentState>(equipmentStateIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateVM>(equipmentStateEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentStateIM), Times.Once);
        }
        [Fact]
        public async Task CreateAsync_InvalidEntity_ThrowsValidationException()
        {
            var equipmentStateIM = new EquipmentStateIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name cannot be empty."),
                new ValidationFailure("Color", "Color cannot be empty.")
            };

            _validatorMock.Setup(v => v.Validate(equipmentStateIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(equipmentStateIM));

            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(equipmentStateIM), Times.Once);
        }
        [Fact]
        public async Task CreateAsync_ExceptionEquipmentStateNull_ThrowsArgumentNullException()
        {

            EquipmentStateIM equipmentStateIM = null;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(equipmentStateIM));
            Assert.Equal("Value cannot be null. (Parameter 'Entity Invalid.')", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var validId = Guid.NewGuid();

            _equipmentRMock.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            await _service.DeleteAsync(validId);

            _equipmentRMock.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {

            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentStateVMs()
        {
            var equipmentStates = new List<EquipmentState>
            {
                new EquipmentState
                {
                    EquipmentStateId = Guid.NewGuid(),
                    Name = "TestName1",
                    Color = "TestColor1",
                    EquipmentStateHistories = new List<EquipmentStateHistory>(),
                    EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
                },
                new EquipmentState
                {
                    EquipmentStateId = Guid.NewGuid(),
                    Name = "TestName2",
                    Color = "TestColor2",
                    EquipmentStateHistories = new List<EquipmentStateHistory>(),
                    EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
                }
            };

            var equipmentStateVMs = new List<EquipmentStateVM>
            {
                new EquipmentStateVM
                {
                    Color = equipmentStates[0].Color,
                    Name = equipmentStates[0].Name,
                    EquipmentStateId = equipmentStates[0].EquipmentStateId
                },
                new EquipmentStateVM
                {
                    Color = equipmentStates[1].Color,
                    Name = equipmentStates[1].Name,
                    EquipmentStateId = equipmentStates[1].EquipmentStateId
                }
            };

            _equipmentRMock.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentStates);

            _mapperMock.Setup(m => m.Map<IEnumerable<EquipmentStateVM>>(equipmentStates))
                .Returns(equipmentStateVMs);

            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentStateVM>>(result);
            Assert.Equal(equipmentStateVMs.Count, result.Count());
            Assert.Contains(result, em => em.Name == "TestName1");
            Assert.Contains(result, em => em.Name == "TestName2");

            _equipmentRMock.Verify(repo => repo.FindAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EquipmentStateVM>>(equipmentStates), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentStateVM()
        {
            var validId = Guid.NewGuid();

            var equipmentState = new EquipmentState
            {
                EquipmentStateId = Guid.NewGuid(),
                Name = "TestName1",
                Color = "TestColor1",
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            var equipmentStateVM = new EquipmentStateVM
            {
                Color = equipmentState.Color,
                Name = equipmentState.Name,
                EquipmentStateId = equipmentState.EquipmentStateId
            };

            _equipmentRMock.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentState);

            _mapperMock.Setup(m => m.Map<EquipmentStateVM>(equipmentState))
                .Returns(equipmentStateVM);

            var result = await _service.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateVM>(result);
            Assert.Equal(equipmentStateVM.Color, result.Color);
            Assert.Equal(equipmentStateVM.Name, result.Name);
            Assert.Equal(equipmentStateVM.EquipmentStateId, result.EquipmentStateId);

            _equipmentRMock.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateVM>(equipmentState), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipment_CallsUpdateAsyncInRepository()
        {
            var equipmentState = new EquipmentState
            {
                EquipmentStateId = Guid.NewGuid(),
                Name = "TestName1",
                Color = "TestColor1",
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            var equipmentStateIM = new EquipmentStateIM
            {
                Name = equipmentState.Name,
                Color = equipmentState.Color
            };

            _validatorMock.Setup(v => v.Validate(equipmentStateIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentState>(equipmentStateIM))
                .Returns(equipmentState);

            _equipmentRMock.Setup(repo => repo.UpdateAsync(equipmentState))
                .Returns(Task.CompletedTask);

            await _service.UpdateAsync(equipmentState.EquipmentStateId, equipmentStateIM);

            _equipmentRMock.Verify(repo => repo.UpdateAsync(equipmentState), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentStateIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentState>(equipmentStateIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidEntity_ThrowsValidationException()
        {
            EquipmentStateIM invalidEquipmentStateIM = new EquipmentStateIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Color", "Color cannot be empty."),
                new ValidationFailure("Name", "Name is required.")
            };

            _validatorMock.Setup(v => v.Validate(invalidEquipmentStateIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentStateIM));
            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(invalidEquipmentStateIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEntity_ThrowsArgumentNullException()
        {
            EquipmentStateIM invalidEquipmentStateIM = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentStateIM));
        }
    }
}

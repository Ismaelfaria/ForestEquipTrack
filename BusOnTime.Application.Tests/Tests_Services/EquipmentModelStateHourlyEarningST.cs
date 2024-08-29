using AutoMapper;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using BusOnTime.Data.Repositories.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Tests_Services
{
    public class EquipmentModelStateHourlyEarningST
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentModelStateHourlyEarningsIM>> _validatorMock;
        private readonly Mock<IEquipmentModelStateHourlyEarningsR> _equipmentModelStateHourlyEarningsRMock;
        private readonly EquipmentModelStateHourlyEarningS _service;

        public EquipmentModelStateHourlyEarningST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            _equipmentModelStateHourlyEarningsRMock = new Mock<IEquipmentModelStateHourlyEarningsR>();

            _service = new EquipmentModelStateHourlyEarningS(_equipmentModelStateHourlyEarningsRMock.Object, _mapperMock.Object, _validatorMock.Object);
        }


        [Fact]
        public async void CreateAsync_ValidEquipmentModelStateHourlyEarning_ReturnsCreatedEquipmentModelStateHourlyEarningsVM()
        {

            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 15
            };

            var equipmentModelStateHourlyEarningEntity = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = equipmentModelStateHourlyEarningsIM.EquipmentModelId,
                EquipmentStateId = equipmentModelStateHourlyEarningsIM.EquipmentStateId,
                Value = equipmentModelStateHourlyEarningsIM.Value,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            var equipmentModelStateHourlyEarningsVM = new EquipmentModelStateHourlyEarningsVM
            {
                EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarningEntity.EquipmentModelStateHourlyEarningsId,
                EquipmentModelId = equipmentModelStateHourlyEarningEntity.EquipmentModelId,
                EquipmentStateId = equipmentModelStateHourlyEarningEntity.EquipmentStateId,
                Value = equipmentModelStateHourlyEarningEntity.Value
            };


            _validatorMock.Setup(v => v.Validate(equipmentModelStateHourlyEarningsIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM))
                .Returns(equipmentModelStateHourlyEarningEntity);

            _mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarningEntity))
                .Returns(equipmentModelStateHourlyEarningsVM);

            _equipmentModelStateHourlyEarningsRMock.Setup(repo => repo.CreateAsync(equipmentModelStateHourlyEarningEntity))
                .ReturnsAsync(equipmentModelStateHourlyEarningEntity);

            var result = await _service.CreateAsync(equipmentModelStateHourlyEarningsIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarningsVM>(result);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.Value, result.Value);

            _equipmentModelStateHourlyEarningsRMock.Verify(repo => repo.CreateAsync(equipmentModelStateHourlyEarningEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarningEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentModelStateHourlyEarningsIM), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ExceptionEquipmentModelStateHourlyEarningsNull_ThrowsArgumentNullException()
        {

            EquipmentModelStateHourlyEarningsIM? equipmentModelStateHourlyEarningsIM = null;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(null));
            Assert.Equal("Value cannot be null. (Parameter 'Entity Invalid.')", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var validId = Guid.NewGuid();

            _equipmentModelStateHourlyEarningsRMock.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            await _service.DeleteAsync(validId);

            _equipmentModelStateHourlyEarningsRMock.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentModelStateHourlyEarningVMs()
        {
            var equipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>
            {
                new EquipmentModelStateHourlyEarnings
                {
                    EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    EquipmentStateId = Guid.NewGuid(),
                    Value = 15,
                    EquipmentModel = new EquipmentModel(),
                    EquipmentState = new EquipmentState()
                },
                new EquipmentModelStateHourlyEarnings
                {
                    EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    EquipmentStateId = Guid.NewGuid(),
                    Value = 16,
                    EquipmentModel = new EquipmentModel(),
                    EquipmentState = new EquipmentState()
                }
            };

            var equipmentModelStateHourlyEarningsVMs = new List<EquipmentModelStateHourlyEarningsVM>
            {
                new EquipmentModelStateHourlyEarningsVM
                {
                    EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarnings[0].EquipmentModelStateHourlyEarningsId,
                    EquipmentModelId = equipmentModelStateHourlyEarnings[0].EquipmentModelId,
                    EquipmentStateId = equipmentModelStateHourlyEarnings[0].EquipmentStateId,
                    Value = equipmentModelStateHourlyEarnings[0].Value
                },
                new EquipmentModelStateHourlyEarningsVM
                {
                    EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarnings[1].EquipmentModelStateHourlyEarningsId,
                    EquipmentModelId = equipmentModelStateHourlyEarnings[1].EquipmentModelId,
                    EquipmentStateId = equipmentModelStateHourlyEarnings[1].EquipmentStateId,
                    Value = equipmentModelStateHourlyEarnings[1].Value
                }
            };

            _equipmentModelStateHourlyEarningsRMock.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentModelStateHourlyEarnings);

            _mapperMock.Setup(m => m.Map<IEnumerable<EquipmentModelStateHourlyEarningsVM>>(equipmentModelStateHourlyEarnings))
                .Returns(equipmentModelStateHourlyEarningsVMs);

            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModelStateHourlyEarningsVM>>(result);
            Assert.Equal(equipmentModelStateHourlyEarningsVMs.Count, result.Count());
            Assert.Contains(result, em => em.Value == 15);
            Assert.Contains(result, em => em.Value == 16);

            _equipmentModelStateHourlyEarningsRMock.Verify(repo => repo.FindAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EquipmentModelStateHourlyEarningsVM>>(equipmentModelStateHourlyEarnings), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentModelStateHourlyEarningsVM()
        {
            var validId = Guid.NewGuid();

            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 16,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            var equipmentModelStateHourlyEarningsVM = new EquipmentModelStateHourlyEarningsVM
            {
                EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId,
                EquipmentModelId = equipmentModelStateHourlyEarnings.EquipmentModelId,
                EquipmentStateId = equipmentModelStateHourlyEarnings.EquipmentStateId,
                Value = equipmentModelStateHourlyEarnings.Value
            };

            _equipmentModelStateHourlyEarningsRMock.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentModelStateHourlyEarnings);

            _mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarnings))
                .Returns(equipmentModelStateHourlyEarningsVM);

            var result = await _service.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarningsVM>(result);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.Value, result.Value);

            _equipmentModelStateHourlyEarningsRMock.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarnings), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentModelStateHourlyEarnings_CallsUpdateAsyncInRepository()
        {
            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 16,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = equipmentModelStateHourlyEarnings.EquipmentModelId,
                EquipmentStateId = equipmentModelStateHourlyEarnings.EquipmentStateId,
                Value = equipmentModelStateHourlyEarnings.Value
            };

            _validatorMock.Setup(v => v.Validate(equipmentModelStateHourlyEarningsIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM))
                .Returns(equipmentModelStateHourlyEarnings);

            _equipmentModelStateHourlyEarningsRMock.Setup(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings))
                .Returns(Task.CompletedTask);

            await _service.UpdateAsync(equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId, equipmentModelStateHourlyEarningsIM);

            _equipmentModelStateHourlyEarningsRMock.Verify(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentModelStateHourlyEarningsIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidEntity_ThrowsValidationExceptionAndArgumentNullException()
        {
            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = Guid.Empty,
                EquipmentStateId = Guid.Empty,
                Value = 0
            };

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentModelId", "EquipmentModelId cannot be empty."),
                new ValidationFailure("EquipmentStateId", "EquipmentStateId cannot be empty.")
            };

            _validatorMock.Setup(v => v.Validate(equipmentModelStateHourlyEarningsIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(Guid.NewGuid(), equipmentModelStateHourlyEarningsIM));
            Assert.Contains("Validation failed", exception.Message);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(Guid.NewGuid(), null));

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(equipmentModelStateHourlyEarningsIM), Times.Once);
        }
    }
}

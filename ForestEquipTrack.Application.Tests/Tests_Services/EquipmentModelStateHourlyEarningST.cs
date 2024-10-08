﻿using AutoMapper;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using ForestEquipTrack.Application.Services;
using ForestEquipTrack.Domain.Entities;
using ForestEquipTrack.Infrastructure.Interfaces.Interface;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Domain.Entities.Enum;
using ForestEquipTrack.Infrastructure.Repositories.Concrete;

namespace Application.Tests.Tests_Services
{
    public class EquipmentModelStateHourlyEarningST
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentModelStateHourlyEarningsIM>> _validatorMock;
        private readonly Mock<IEquipmentModelStateHourlyEarningsR> _equipmentModelStateHourlyEarningsRMock;
        private readonly Mock<IEquipmentModelS> _equipmentModelS;
        private readonly EquipmentModelStateHourlyEarningS _service;

        public EquipmentModelStateHourlyEarningST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            _equipmentModelS = new Mock<IEquipmentModelS>();
            _equipmentModelStateHourlyEarningsRMock = new Mock<IEquipmentModelStateHourlyEarningsR>();
           
            _service = new EquipmentModelStateHourlyEarningS(_equipmentModelS.Object,_equipmentModelStateHourlyEarningsRMock.Object , _mapperMock.Object, _validatorMock.Object);
        }

        #nullable enable
        [Fact]
        public async void CreateAsync_ValidEquipmentModelStateHourlyEarning_ReturnsCreatedEquipmentModelStateHourlyEarningsVM()
        {

            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = Guid.NewGuid(),
                Status = EquipmentStateType.Parado,
                Value = 15
            };

            var equipmentModelStateHourlyEarningEntity = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = equipmentModelStateHourlyEarningsIM.EquipmentModelId,
                Status = equipmentModelStateHourlyEarningsIM.Status,
                ModelName = "Makita",
                Value = equipmentModelStateHourlyEarningsIM.Value,
                EquipmentModel = new EquipmentModel()
            };

            var equipmentModelStateHourlyEarningsVM = new EquipmentModelStateHourlyEarningsVM
            {
                EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarningEntity.EquipmentModelStateHourlyEarningsId,
                EquipmentModelId = equipmentModelStateHourlyEarningEntity.EquipmentModelId,
                ModelName = equipmentModelStateHourlyEarningEntity.ModelName,
                Status = equipmentModelStateHourlyEarningEntity.Status,
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

            _equipmentModelS.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EquipmentModelVM
                {
                    Name = "Makita"
                });

            var result = await _service.CreateAsync(equipmentModelStateHourlyEarningsIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarningsVM>(result);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.ModelName, result.ModelName);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.Status, result.Status);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.Value, result.Value);

            _equipmentModelStateHourlyEarningsRMock.Verify(repo => repo.CreateAsync(equipmentModelStateHourlyEarningEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarningEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentModelStateHourlyEarningsIM), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_InvalidEntity_ThrowsValidationException()
        {
            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentModelId", "EquipmentModelId cannot be empty."),
                new ValidationFailure("EquipmentStateId", "EquipmentStateId cannot be empty.")
            };

            _validatorMock.Setup(v => v.Validate(equipmentModelStateHourlyEarningsIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(equipmentModelStateHourlyEarningsIM));

            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(equipmentModelStateHourlyEarningsIM), Times.Once);
        }
        
        [Fact]
        public async Task CreateAsync_ExceptionEquipmentModelStateHourlyEarningsNull_ThrowsArgumentNullException()
        {

            EquipmentModelStateHourlyEarningsIM? equipmentModelStateHourlyEarningsIM = null;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(equipmentModelStateHourlyEarningsIM));
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
                    ModelName = "Makita",
                    Status = EquipmentStateType.Operando,
                    Value = 15,
                    EquipmentModel = new EquipmentModel()
                },
                new EquipmentModelStateHourlyEarnings
                {
                    EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    ModelName = "Furadeira",
                    Status= EquipmentStateType.Parado,
                    Value = 16,
                    EquipmentModel = new EquipmentModel()
                }
            };

            var equipmentModelStateHourlyEarningsVMs = new List<EquipmentModelStateHourlyEarningsVM>
            {
                new EquipmentModelStateHourlyEarningsVM
                {
                    EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarnings[0].EquipmentModelStateHourlyEarningsId,
                    EquipmentModelId = equipmentModelStateHourlyEarnings[0].EquipmentModelId,
                    ModelName = equipmentModelStateHourlyEarnings[0].ModelName,
                    Status = equipmentModelStateHourlyEarnings[0].Status,
                    Value = equipmentModelStateHourlyEarnings[0].Value
                },
                new EquipmentModelStateHourlyEarningsVM
                {
                    EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarnings[1].EquipmentModelStateHourlyEarningsId,
                    EquipmentModelId = equipmentModelStateHourlyEarnings[1].EquipmentModelId,
                    ModelName = equipmentModelStateHourlyEarnings[1].ModelName,
                    Status = equipmentModelStateHourlyEarnings[1].Status,
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
                ModelName = "Makita",
                Status = EquipmentStateType.Operando,
                Value = 16,
                EquipmentModel = new EquipmentModel()
            };

            var equipmentModelStateHourlyEarningsVM = new EquipmentModelStateHourlyEarningsVM
            {
                EquipmentModelStateHourlyEarningsId = equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId,
                EquipmentModelId = equipmentModelStateHourlyEarnings.EquipmentModelId,
                ModelName = equipmentModelStateHourlyEarnings.ModelName,
                Status = equipmentModelStateHourlyEarnings.Status,
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
                ModelName = "Furadeira",
                Status = EquipmentStateType.Operando,
                Value = 16,
                EquipmentModel = new EquipmentModel()
            };

            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = equipmentModelStateHourlyEarnings.EquipmentModelId,
                Status = EquipmentStateType.Operando,
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
        public async Task UpdateAsync_InvalidEntity_ThrowsValidationException()
        {
            EquipmentModelStateHourlyEarningsIM invalidEquipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentModelId", "EquipmentModelId cannot be empty."),
                new ValidationFailure("EquipmentStateId", "EquipmentStateId cannot be empty.")
            };

            _validatorMock.Setup(v => v.Validate(invalidEquipmentModelStateHourlyEarningsIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentModelStateHourlyEarningsIM));
            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(invalidEquipmentModelStateHourlyEarningsIM), Times.Once);
        }
        
        [Fact]
        public async Task UpdateAsync_NullEntity_ThrowsArgumentNullException()
        {
            EquipmentModelStateHourlyEarningsIM invalidEquipmentModelStateHourlyEarningsIM = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentModelStateHourlyEarningsIM));
        }
        #nullable restore
    }
}

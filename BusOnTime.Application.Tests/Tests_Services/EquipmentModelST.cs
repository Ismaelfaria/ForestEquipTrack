﻿using AutoMapper;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
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
    public class EquipmentModelST
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentModelIM>> _validatorMock;
        private readonly Mock<IEquipmentModelR> _equipmentModelRMock;
        private readonly EquipmentModelS _service;

        public EquipmentModelST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentModelIM>>();
            _equipmentModelRMock = new Mock<IEquipmentModelR>();

            _service = new EquipmentModelS(_equipmentModelRMock.Object, _mapperMock.Object, _validatorMock.Object);
        }


        [Fact]
        public async void CreateAsync_ValidEquipmentModel_ReturnsCreatedEquipmentModelVM()
        {

            var equipmentModelIM = new EquipmentModelIM
            {
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator"
            };

            var equipmentModelEntity = new EquipmentModel
            {
                ModelId = Guid.NewGuid(),
                EquipmentId = equipmentModelIM.EquipmentId,
                Name = equipmentModelIM.Name,
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            var equipmentModelVM = new EquipmentModelVM
            {
                ModelId = equipmentModelEntity.ModelId,
                EquipmentId = equipmentModelEntity.EquipmentId,
                Name = equipmentModelEntity.Name
            };


            _validatorMock.Setup(v => v.Validate(equipmentModelIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentModel>(equipmentModelIM))
                .Returns(equipmentModelEntity);

            _mapperMock.Setup(m => m.Map<EquipmentModelVM>(equipmentModelEntity))
                .Returns(equipmentModelVM);

            _equipmentModelRMock.Setup(repo => repo.CreateAsync(equipmentModelEntity))
                .ReturnsAsync(equipmentModelEntity);

            var result = await _service.CreateAsync(equipmentModelIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelVM>(result);
            Assert.Equal(equipmentModelVM.ModelId, result.ModelId);
            Assert.Equal(equipmentModelVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModelVM.Name, result.Name);

            _equipmentModelRMock.Verify(repo => repo.CreateAsync(equipmentModelEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModel>(equipmentModelIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelVM>(equipmentModelEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentModelIM), Times.Once);
        }
        
        [Fact]
        public async Task CreateAsync_ExceptionEquipmentModelNull_ThrowsArgumentNullException()
        {

            EquipmentModelIM? equipmentModelIM = null;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(equipmentModelIM));
            Assert.Equal("Value cannot be null. (Parameter 'Entity Invalid.')", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var validId = Guid.NewGuid();

            _equipmentModelRMock.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            await _service.DeleteAsync(validId);

            _equipmentModelRMock.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            
            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentModelVMs()
        {
            var equipmentModels = new List<EquipmentModel>
            {
                new EquipmentModel
                {
                    ModelId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Name = "Excavator",
                    Equipment = new List<Equipment>(),
                    EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
                },
                new EquipmentModel
                {
                    ModelId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Name = "Bulldozer",
                    Equipment = new List<Equipment>(),
                    EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
                }
            };

            var equipmentModelVMs = new List<EquipmentModelVM>
            {
                new EquipmentModelVM
                {
                    ModelId = equipmentModels[0].ModelId,
                    EquipmentId = equipmentModels[0].EquipmentId,
                    Name = equipmentModels[0].Name
                },
                new EquipmentModelVM
                {
                    ModelId = equipmentModels[1].ModelId,
                    EquipmentId = equipmentModels[1].EquipmentId,
                    Name = equipmentModels[1].Name
                }
            };

            _equipmentModelRMock.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentModels);

            _mapperMock.Setup(m => m.Map<IEnumerable<EquipmentModelVM>>(equipmentModels))
                .Returns(equipmentModelVMs);

            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModelVM>>(result);
            Assert.Equal(equipmentModelVMs.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Excavator");
            Assert.Contains(result, em => em.Name == "Bulldozer");

            _equipmentModelRMock.Verify(repo => repo.FindAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EquipmentModelVM>>(equipmentModels), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentModelVM()
        {
            var validId = Guid.NewGuid();

            var equipmentModel = new EquipmentModel
            {
                ModelId = validId,
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator",
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            var equipmentModelVM = new EquipmentModelVM
            {
                ModelId = equipmentModel.ModelId,
                EquipmentId = equipmentModel.EquipmentId,
                Name = equipmentModel.Name
            };

            _equipmentModelRMock.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentModel);

            _mapperMock.Setup(m => m.Map<EquipmentModelVM>(equipmentModel))
                .Returns(equipmentModelVM);

            var result = await _service.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelVM>(result);
            Assert.Equal(equipmentModelVM.ModelId, result.ModelId);
            Assert.Equal(equipmentModelVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModelVM.Name, result.Name);

            _equipmentModelRMock.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModelVM>(equipmentModel), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentModel_CallsUpdateAsyncInRepository()
        {
            var equipmentModel = new EquipmentModel
            {
                ModelId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator",
                Equipment = new List<Equipment>(),
                EquipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>()
            };

            var equipmentModelIM = new EquipmentModelIM
            {
                EquipmentId = equipmentModel.EquipmentId,
                Name = equipmentModel.Name
            };

            _validatorMock.Setup(v => v.Validate(equipmentModelIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentModel>(equipmentModelIM))
                .Returns(equipmentModel);

            _equipmentModelRMock.Setup(repo => repo.UpdateAsync(equipmentModel))
                .Returns(Task.CompletedTask);

            await _service.UpdateAsync(equipmentModel.ModelId, equipmentModelIM);

            _equipmentModelRMock.Verify(repo => repo.UpdateAsync(equipmentModel), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentModelIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentModel>(equipmentModelIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidEntity_ThrowsValidationExceptionAndArgumentNullException()
        {
            var invalidEquipmentModelIM = new EquipmentModelIM
            {
                EquipmentId = Guid.Empty,
                Name = null
            };

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentId", "EquipmentId cannot be empty."),
                new ValidationFailure("Name", "Name is required.")
            };

            _validatorMock.Setup(v => v.Validate(invalidEquipmentModelIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentModelIM));
            Assert.Contains("Validation failed", exception.Message);
            
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(Guid.NewGuid(), null));

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(invalidEquipmentModelIM), Times.Once);
        }
    }
}

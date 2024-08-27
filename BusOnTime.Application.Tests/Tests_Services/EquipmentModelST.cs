using AutoMapper;
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
        [Fact]
        public async void CreateAsync_ValidEquipmentModel_ReturnsCreatedEquipmentModelVM()
        {
            var equipmentModelRepository = new Mock<IEquipmentModelR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentModelIM>>();

            var equipmentModelIM = new EquipmentModelIM
            {
                EquipmentId = Guid.NewGuid(),
                Name = "Excavator",
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

            validator.Setup(v => v.Validate(equipmentModelIM))
                .Returns(new ValidationResult());

            mapper.Setup(m => m.Map<EquipmentModel>(equipmentModelIM))
                .Returns(equipmentModelEntity);

            mapper.Setup(m => m.Map<EquipmentModelVM>(equipmentModelEntity))
                .Returns(equipmentModelVM);

            equipmentModelRepository.Setup(repo => repo.CreateAsync(equipmentModelEntity))
                .ReturnsAsync(equipmentModelEntity);

            var equipmentModelService = new EquipmentModelS(equipmentModelRepository.Object, mapper.Object, validator.Object);

            var result = await equipmentModelService.CreateAsync(equipmentModelIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelVM>(result);
            Assert.Equal(equipmentModelVM.ModelId, result.ModelId);
            Assert.Equal(equipmentModelVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModelVM.Name, result.Name);

            equipmentModelRepository.Verify(repo => repo.CreateAsync(equipmentModelEntity), Times.Once);
            mapper.Verify(m => m.Map<EquipmentModel>(equipmentModelIM), Times.Once);
            mapper.Verify(m => m.Map<EquipmentModelVM>(equipmentModelEntity), Times.Once);
            validator.Verify(v => v.Validate(equipmentModelIM), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

            var validId = Guid.NewGuid();

            mockEquipmentModelRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            await equipmentModelService.DeleteAsync(validId);

            mockEquipmentModelRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

            var invalidId = Guid.Empty;

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentModelVMs()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

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

            mockEquipmentModelRepository.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentModels);

            mockMapper.Setup(m => m.Map<IEnumerable<EquipmentModelVM>>(equipmentModels))
                .Returns(equipmentModelVMs);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            var result = await equipmentModelService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModelVM>>(result);
            Assert.Equal(equipmentModelVMs.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Excavator");
            Assert.Contains(result, em => em.Name == "Bulldozer");

            mockEquipmentModelRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
            mockMapper.Verify(m => m.Map<IEnumerable<EquipmentModelVM>>(equipmentModels), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentModelVM()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

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

            mockEquipmentModelRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentModel);

            mockMapper.Setup(m => m.Map<EquipmentModelVM>(equipmentModel))
                .Returns(equipmentModelVM);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            var result = await equipmentModelService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelVM>(result);
            Assert.Equal(equipmentModelVM.ModelId, result.ModelId);
            Assert.Equal(equipmentModelVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModelVM.Name, result.Name);

            mockEquipmentModelRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            mockMapper.Verify(m => m.Map<EquipmentModelVM>(equipmentModel), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

            var invalidId = Guid.Empty;

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentModel_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

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

            mockValidator.Setup(v => v.Validate(equipmentModelIM))
                .Returns(new ValidationResult());

            mockMapper.Setup(m => m.Map<EquipmentModel>(equipmentModelIM))
                .Returns(equipmentModel);

            mockEquipmentModelRepository.Setup(repo => repo.UpdateAsync(equipmentModel))
                .Returns(Task.CompletedTask);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            await equipmentModelService.UpdateAsync(equipmentModel.ModelId, equipmentModelIM);

            mockEquipmentModelRepository.Verify(repo => repo.UpdateAsync(equipmentModel), Times.Once);
            mockValidator.Verify(v => v.Validate(equipmentModelIM), Times.Once);
            mockMapper.Verify(m => m.Map<EquipmentModel>(equipmentModelIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidEquipmentModel_ThrowsValidationException()
        {
            // Arrange
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

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

            mockValidator.Setup(v => v.Validate(invalidEquipmentModelIM))
                         .Returns(new ValidationResult(validationFailures));

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => equipmentModelService.UpdateAsync(Guid.NewGuid(), invalidEquipmentModelIM));
            Assert.Contains("Validation failed", exception.Message);
            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            mockValidator.Verify(v => v.Validate(invalidEquipmentModelIM), Times.Once);
        }
    }
}

using AutoMapper;
using BusOnTime.Application.Mapping.DTOs.InputModel;
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
        public async void CreateAsync_ValidEquipmentModel_ReturnsCreatedEquipmentModel()
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

            validator.Setup(v => v.Validate(equipmentModelIM))
                .Returns(new ValidationResult());

            mapper.Setup(m => m.Map<EquipmentModel>(equipmentModelIM))
                .Returns(equipmentModelEntity);

            equipmentModelRepository.Setup(repo => repo.CreateAsync(equipmentModelEntity))
                .ReturnsAsync(equipmentModelEntity);

            var equipmentModelService = new EquipmentModelS(equipmentModelRepository.Object, mapper.Object, validator.Object);

            var result = await equipmentModelService.CreateAsync(equipmentModelIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModel>(result);
            Assert.Equal(equipmentModelEntity.ModelId, result.ModelId);
            Assert.Equal(equipmentModelEntity.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModelEntity.Name, result.Name);

            equipmentModelRepository.Verify(repo => repo.CreateAsync(equipmentModelEntity), Times.Once);
            mapper.Verify(m => m.Map<EquipmentModel>(equipmentModelIM), Times.Once);
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
        public async Task FindAllAsync_ReturnsListOfEquipmentModels()
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

            mockEquipmentModelRepository.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentModels);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            var result = await equipmentModelService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModel>>(result);
            Assert.Equal(equipmentModels.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Excavator");
            Assert.Contains(result, em => em.Name == "Bulldozer");

            mockEquipmentModelRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentModel()
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

            mockEquipmentModelRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentModel);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            var result = await equipmentModelService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModel>(result);
            Assert.Equal(equipmentModel.ModelId, result.ModelId);
            Assert.Equal(equipmentModel.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentModel.Name, result.Name);
            Assert.Equal(equipmentModel.Equipment, result.Equipment);
            Assert.Equal(equipmentModel.EquipmentModelStateHourlyEarnings, result.EquipmentModelStateHourlyEarnings);

            mockEquipmentModelRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
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

            mockEquipmentModelRepository.Setup(repo => repo.UpdateAsync(equipmentModel))
                .Returns(Task.CompletedTask);

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            await equipmentModelService.UpdateAsync(equipmentModel);

            mockEquipmentModelRepository.Verify(repo => repo.UpdateAsync(equipmentModel), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentModel_ThrowsArgumentNullException()
        {
            var mockEquipmentModelRepository = new Mock<IEquipmentModelR>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EquipmentModelIM>>();

            EquipmentModel? nullEquipmentModel = null;

            var equipmentModelService = new EquipmentModelS(mockEquipmentModelRepository.Object, mockMapper.Object, mockValidator.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentModelService.UpdateAsync(nullEquipmentModel));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

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
    public class EquipmentST
    {
        [Fact]
        public async Task CreateAsync_ValidEquipment_ReturnsCreatedEquipment()
        {
            // Arrange
            var equipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();

            var equipmentEntity = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Excavator",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            var equipmentIM = new EquipmentIM
            {
                EquipmentModelId = Guid.NewGuid(),
                Name = "Excavator"
            };

            validator.Setup(v => v.Validate(It.IsAny<EquipmentIM>()))
                     .Returns(new FluentValidation.Results.ValidationResult());

            mapper.Setup(m => m.Map<Equipment>(It.IsAny<EquipmentIM>()))
                  .Returns(equipmentEntity);
            mapper.Setup(m => m.Map<EquipmentVM>(It.IsAny<Equipment>()))
                  .Returns(new EquipmentVM
                  {
                      EquipmentId = equipmentEntity.EquipmentId,
                      EquipmentModelId = equipmentEntity.EquipmentModelId,
                      Name = equipmentEntity.Name,
                      EquipmentModel = equipmentEntity.EquipmentModel,
                      EquipmentStateHistories = equipmentEntity.EquipmentStateHistories,
                      EquipmentPositionHistories = equipmentEntity.EquipmentPositionHistories
                  });

            equipmentRepository.Setup(repo => repo.CreateAsync(equipmentEntity))
                               .ReturnsAsync(equipmentEntity);

            var equipmentService = new EquipmentS(equipmentRepository.Object, mapper.Object, validator.Object);

            // Act
            var result = await equipmentService.CreateAsync(equipmentIM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentVM>(result);
            Assert.Equal(equipmentEntity.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentEntity.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentEntity.Name, result.Name);
            Assert.Equal(equipmentEntity.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipmentEntity.EquipmentStateHistories, result.EquipmentStateHistories);
            Assert.Equal(equipmentEntity.EquipmentPositionHistories, result.EquipmentPositionHistories);

            equipmentRepository.Verify(repo => repo.CreateAsync(equipmentEntity), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            // Arrange
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();
            var validId = Guid.NewGuid();

            mockEquipmentRepository.Setup(repo => repo.DeleteAsync(validId))
                                   .Returns(Task.CompletedTask);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object, mapper.Object, validator.Object);

            // Act
            await equipmentService.DeleteAsync(validId);

            // Assert
            mockEquipmentRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();
            var invalidId = Guid.Empty;

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object, mapper.Object, validator.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipments()
        {
            // Arrange
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();
            var equipments = new List<Equipment>
            {
                new Equipment
                {
                    EquipmentId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    Name = "Excavator",
                    EquipmentModel = new EquipmentModel(),
                    EquipmentStateHistories = new List<EquipmentStateHistory>(),
                    EquipmentPositionHistories = new List<EquipmentPositionHistory>()
                },
                new Equipment
                {
                    EquipmentId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    Name = "Bulldozer",
                    EquipmentModel = new EquipmentModel(),
                    EquipmentStateHistories = new List<EquipmentStateHistory>(),
                    EquipmentPositionHistories = new List<EquipmentPositionHistory>()
                }
            };

            var equipmentVMs = equipments.Select(e => new EquipmentVM
            {
                EquipmentId = e.EquipmentId,
                EquipmentModelId = e.EquipmentModelId,
                Name = e.Name,
                EquipmentModel = e.EquipmentModel,
                EquipmentStateHistories = e.EquipmentStateHistories,
                EquipmentPositionHistories = e.EquipmentPositionHistories
            }).ToList();

            mockEquipmentRepository.Setup(repo => repo.FindAllAsync())
                                   .ReturnsAsync(equipments);
            mapper.Setup(m => m.Map<IEnumerable<EquipmentVM>>(It.IsAny<IEnumerable<Equipment>>()))
                  .Returns(equipmentVMs);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object, mapper.Object, validator.Object);

            // Act
            var result = await equipmentService.FindAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<EquipmentVM>>(result);
            Assert.Equal(equipments.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Excavator");
            Assert.Contains(result, em => em.Name == "Bulldozer");

            mockEquipmentRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipment()
        {
            // Arrange
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();
            var validId = Guid.NewGuid();
            var equipment = new Equipment
            {
                EquipmentId = validId,
                EquipmentModelId = Guid.NewGuid(),
                Name = "Bulldozer",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            var equipmentVM = new EquipmentVM
            {
                EquipmentId = validId,
                EquipmentModelId = equipment.EquipmentModelId,
                Name = equipment.Name,
                EquipmentModel = equipment.EquipmentModel,
                EquipmentStateHistories = equipment.EquipmentStateHistories,
                EquipmentPositionHistories = equipment.EquipmentPositionHistories
            };

            mockEquipmentRepository.Setup(repo => repo.GetByIdAsync(validId))
                                   .ReturnsAsync(equipment);
            mapper.Setup(m => m.Map<EquipmentVM>(It.IsAny<Equipment>()))
                  .Returns(equipmentVM);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object, mapper.Object, validator.Object);

            // Act
            var result = await equipmentService.GetByIdAsync(validId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EquipmentVM>(result);
            Assert.Equal(equipment.EquipmentId, result.EquipmentId);
            Assert.Equal(equipment.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipment.Name, result.Name);
            Assert.Equal(equipment.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipment.EquipmentStateHistories, result.EquipmentStateHistories);
            Assert.Equal(equipment.EquipmentPositionHistories, result.EquipmentPositionHistories);

            mockEquipmentRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();
            var invalidId = Guid.Empty;

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object, mapper.Object, validator.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipment_CallsUpdateAsyncInRepository()
        {
            // Arrange
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();
            var equipment = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Excavator",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            var equipmentIM = new EquipmentIM
            {
                EquipmentModelId = equipment.EquipmentModelId,
                Name = equipment.Name
            };

            mapper.Setup(m => m.Map<Equipment>(It.IsAny<EquipmentIM>()))
                  .Returns(equipment);

            mockEquipmentRepository.Setup(repo => repo.UpdateAsync(equipment))
                                   .Returns(Task.CompletedTask);

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object, mapper.Object, validator.Object);

            // Act
            await equipmentService.UpdateAsync(equipment.EquipmentId,equipmentIM);

            // Assert
            mockEquipmentRepository.Verify(repo => repo.UpdateAsync(equipment), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipment_ThrowsArgumentNullException()
        {
            // Arrange
            var mockEquipmentRepository = new Mock<IEquipmentR>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<EquipmentIM>>();

            var equipmentService = new EquipmentS(mockEquipmentRepository.Object, mapper.Object, validator.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentService.UpdateAsync(Guid.Empty,null));
            Assert.Equal("EquipmentIM cannot be null", exception.Message);
        }
    }
}

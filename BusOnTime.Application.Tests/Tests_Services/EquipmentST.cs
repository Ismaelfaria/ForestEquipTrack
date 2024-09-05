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
    public class EquipmentST
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentIM>> _validatorMock;
        private readonly Mock<IEquipmentR> _equipmentRMock;
        private readonly EquipmentS _service;

        public EquipmentST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentIM>>();
            _equipmentRMock = new Mock<IEquipmentR>();

            _service = new EquipmentS(_equipmentRMock.Object, _mapperMock.Object, _validatorMock.Object);
        }


        [Fact]
        public async void CreateAsync_ValidEquipment_ReturnsCreatedEquipmentVM()
        {

            var equipmentIM = new EquipmentIM
            {
                EquipmentModelId = Guid.NewGuid(),
                Name = "Excavator"
            };

            var equipmentEntity = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = equipmentIM.EquipmentModelId,
                Name = equipmentIM.Name,
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            var equipmentVM = new EquipmentVM
            {
                EquipmentModelId = equipmentEntity.EquipmentModelId,
                EquipmentId = equipmentEntity.EquipmentId,
                Name = equipmentEntity.Name
            };


            _validatorMock.Setup(v => v.Validate(equipmentIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<Equipment>(equipmentIM))
                .Returns(equipmentEntity);

            _mapperMock.Setup(m => m.Map<EquipmentVM>(equipmentEntity))
                .Returns(equipmentVM);

            _equipmentRMock.Setup(repo => repo.CreateAsync(equipmentEntity))
                .ReturnsAsync(equipmentEntity);

            var result = await _service.CreateAsync(equipmentIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentVM>(result);
            Assert.Equal(equipmentVM.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentVM.Name, result.Name);

            _equipmentRMock.Verify(repo => repo.CreateAsync(equipmentEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<Equipment>(equipmentIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentVM>(equipmentEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentIM), Times.Once);
        }
        [Fact]
        public async Task CreateAsync_InvalidEntity_ThrowsValidationException()
        {
            var equipmentIM = new EquipmentIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentModelId", "EquipmentModelId cannot be empty."),
                new ValidationFailure("Name", "Name cannot be empty.")
            };

            _validatorMock.Setup(v => v.Validate(equipmentIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(equipmentIM));

            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(equipmentIM), Times.Once);
        }
        [Fact]
        public async Task CreateAsync_ExceptionEquipmentNull_ThrowsArgumentNullException()
        {

            EquipmentIM equipmentIM = null;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(equipmentIM));
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
        public async Task FindAllAsync_ReturnsListOfEquipmentVMs()
        {
            var equipments = new List<Equipment>
            {
                new Equipment
                {
                    EquipmentId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    Name = "Test1",
                    EquipmentModel = new EquipmentModel(),
                    EquipmentStateHistories = new List<EquipmentStateHistory>(),
                    EquipmentPositionHistories = new List<EquipmentPositionHistory>()
                },
                new Equipment
                {
                    EquipmentId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    Name = "Test2",
                    EquipmentModel = new EquipmentModel(),
                    EquipmentStateHistories = new List<EquipmentStateHistory>(),
                    EquipmentPositionHistories = new List<EquipmentPositionHistory>()
                }
            };

            var equipmentVMs = new List<EquipmentVM>
            {
                new EquipmentVM
                {
                    EquipmentModelId = equipments[0].EquipmentModelId,
                    EquipmentId = equipments[0].EquipmentId,
                    Name = equipments[0].Name
                },
                new EquipmentVM
                {
                    EquipmentModelId = equipments[1].EquipmentModelId,
                    EquipmentId = equipments[1].EquipmentId,
                    Name = equipments[1].Name
                }
            };

            _equipmentRMock.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipments);

            _mapperMock.Setup(m => m.Map<IEnumerable<EquipmentVM>>(equipments))
                .Returns(equipmentVMs);

            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentVM>>(result);
            Assert.Equal(equipmentVMs.Count, result.Count());
            Assert.Contains(result, em => em.Name == "Test1");
            Assert.Contains(result, em => em.Name == "Test2");

            _equipmentRMock.Verify(repo => repo.FindAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EquipmentVM>>(equipments), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentVM()
        {
            var validId = Guid.NewGuid();

            var equipment = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Test1",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            var equipmentVM = new EquipmentVM
            {
                EquipmentModelId = equipment.EquipmentModelId,
                EquipmentId = equipment.EquipmentId,
                Name = equipment.Name
            };

            _equipmentRMock.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipment);

            _mapperMock.Setup(m => m.Map<EquipmentVM>(equipment))
                .Returns(equipmentVM);

            var result = await _service.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentVM>(result);
            Assert.Equal(equipmentVM.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentVM.Name, result.Name);

            _equipmentRMock.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentVM>(equipment), Times.Once);
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
            var equipment = new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                Name = "Test1",
                EquipmentModel = new EquipmentModel(),
                EquipmentStateHistories = new List<EquipmentStateHistory>(),
                EquipmentPositionHistories = new List<EquipmentPositionHistory>()
            };

            var equipmentIM = new EquipmentIM
            {
                EquipmentModelId = equipment.EquipmentModelId,
                Name = equipment.Name
            };

            _validatorMock.Setup(v => v.Validate(equipmentIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<Equipment>(equipmentIM))
                .Returns(equipment);

            _equipmentRMock.Setup(repo => repo.UpdateAsync(equipment))
                .Returns(Task.CompletedTask);

            await _service.UpdateAsync(equipment.EquipmentId, equipmentIM);

            _equipmentRMock.Verify(repo => repo.UpdateAsync(equipment), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentIM), Times.Once);
            _mapperMock.Verify(m => m.Map<Equipment>(equipmentIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidEntity_ThrowsValidationException()
        {
            EquipmentIM invalidEquipmentIM = new EquipmentIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentModelId", "EquipmentModelId cannot be empty."),
                new ValidationFailure("Name", "Name is required.")
            };

            _validatorMock.Setup(v => v.Validate(invalidEquipmentIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentIM));
            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(invalidEquipmentIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEntity_ThrowsArgumentNullException()
        {
            EquipmentIM invalidEquipmentIM = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentIM));
        }
    }
}

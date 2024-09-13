using AutoMapper;
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

namespace Application.Tests.Tests_Services
{
    public class EquipmentStateHistoryST
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentStateHistoryIM>> _validatorMock;
        private readonly Mock<IEquipmentStateHistoryR> _equipmentRMock;
        private readonly Mock<IEquipmentS> _equipmentS;

        private readonly EquipmentStateHistoryS _service;

        public EquipmentStateHistoryST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentStateHistoryIM>>();
            _equipmentRMock = new Mock<IEquipmentStateHistoryR>();
            _equipmentS = new Mock<IEquipmentS>();

            _service = new EquipmentStateHistoryS(_equipmentS.Object , _equipmentRMock.Object, _mapperMock.Object, _validatorMock.Object);

        }


        [Fact]
        public async void CreateAsync_ValidEquipment_ReturnsCreatedEquipmentStateHistoryVM()
        {
            var equipmentStateHistoryIM = new EquipmentStateHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                Status = EquipmentStateType.Operando,
                Date = DateTime.Now
            };

            var equipmentStateHistoryEntity = new EquipmentStateHistory
            {
                EquipmentStateHistoryId = Guid.NewGuid(),
                EquipmentId = equipmentStateHistoryIM.EquipmentId,
                EquipmentName = "equipamento",
                Status = equipmentStateHistoryIM.Status,
                Date = equipmentStateHistoryIM.Date,
                Equipment = new Equipment()
            };

            var equipmentStateHistoryVM = new EquipmentStateHistoryVM
            {
                EquipmentStatehistoryId = equipmentStateHistoryEntity.EquipmentStateHistoryId,
                EquipmentId = equipmentStateHistoryEntity.EquipmentId,
                EquipmentName = equipmentStateHistoryEntity.EquipmentName,
                Status = equipmentStateHistoryEntity.Status,
                Date = equipmentStateHistoryEntity.Date
            };

            _validatorMock.Setup(v => v.Validate(equipmentStateHistoryIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentStateHistory>(equipmentStateHistoryIM))
                .Returns(equipmentStateHistoryEntity);

            _mapperMock.Setup(m => m.Map<EquipmentStateHistoryVM>(equipmentStateHistoryEntity))
                .Returns(equipmentStateHistoryVM);

            _equipmentRMock.Setup(repo => repo.CreateAsync(equipmentStateHistoryEntity))
                .ReturnsAsync(equipmentStateHistoryEntity);

            _equipmentS.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EquipmentVM
                {
                    Name = "equipamento"
                });

            var result = await _service.CreateAsync(equipmentStateHistoryIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistoryVM>(result);
            Assert.Equal(equipmentStateHistoryVM.EquipmentStatehistoryId, result.EquipmentStatehistoryId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentName, result.EquipmentName);
            Assert.Equal(equipmentStateHistoryVM.Status, result.Status);
            Assert.Equal(equipmentStateHistoryVM.Date, result.Date);

            _equipmentRMock.Verify(repo => repo.CreateAsync(equipmentStateHistoryEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateHistory>(equipmentStateHistoryIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateHistoryVM>(equipmentStateHistoryEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentStateHistoryIM), Times.Once);
            _equipmentS.Verify(s => s.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_InvalidEntity_ThrowsValidationException()
        {
            var equipmentStateHistoryIM = new EquipmentStateHistoryIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentId", "EquipmentId cannot be empty."),
                new ValidationFailure("EquipmentStateId", "EquipmentStateId cannot be empty."),
                new ValidationFailure("Date", "Date cannot be empty.")
            };

            _validatorMock.Setup(v => v.Validate(equipmentStateHistoryIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(equipmentStateHistoryIM));

            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(equipmentStateHistoryIM), Times.Once);
        }
        [Fact]
        public async Task CreateAsync_ExceptionEquipmentStateHistoryNull_ThrowsArgumentNullException()
        {

            EquipmentStateHistoryIM equipmentStateHistoryIM = null;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(equipmentStateHistoryIM));
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
        public async Task FindAllAsync_ReturnsListOfEquipmentStateHistoryVMs()
        {
            var equipmentStateHistorys = new List<EquipmentStateHistory>
            {
                new EquipmentStateHistory
                {
                    EquipmentStateHistoryId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = new DateTime(2023, 1, 1, 15, 30, 0),
                    EquipmentName = "equipamento",
                    Status = EquipmentStateType.Operando,
                    Equipment = new Equipment(),
                },
                new EquipmentStateHistory
                {
                    EquipmentStateHistoryId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = new DateTime(2024, 1, 1, 15, 30, 0),
                    EquipmentName = "equipamento2",
                    Status = EquipmentStateType.Manutenção,
                    Equipment = new Equipment(),
                }
            };

            var equipmentStateHistoryVMs = new List<EquipmentStateHistoryVM>
            {
                new EquipmentStateHistoryVM
                {
                    EquipmentStatehistoryId = equipmentStateHistorys[0].EquipmentStateHistoryId,
                    EquipmentId = equipmentStateHistorys[0].EquipmentId,
                    EquipmentName = equipmentStateHistorys[0].EquipmentName,
                    Status = equipmentStateHistorys[0].Status,
                    Date = equipmentStateHistorys[0].Date
                },
                new EquipmentStateHistoryVM
                {
                    EquipmentStatehistoryId = equipmentStateHistorys[1].EquipmentStateHistoryId,
                    EquipmentId = equipmentStateHistorys[1].EquipmentId,
                    EquipmentName = equipmentStateHistorys[1].EquipmentName,
                    Status = equipmentStateHistorys[1].Status,
                    Date = equipmentStateHistorys[1].Date
                }
            };

            _equipmentRMock.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentStateHistorys);

            _mapperMock.Setup(m => m.Map<IEnumerable<EquipmentStateHistoryVM>>(equipmentStateHistorys))
                .Returns(equipmentStateHistoryVMs);

            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentStateHistoryVM>>(result);
            Assert.Equal(equipmentStateHistoryVMs.Count, result.Count());
            Assert.Contains(result, em => em.Date == new DateTime(2023, 1, 1, 15, 30, 0));
            Assert.Contains(result, em => em.Date == new DateTime(2024, 1, 1, 15, 30, 0));

            _equipmentRMock.Verify(repo => repo.FindAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EquipmentStateHistoryVM>>(equipmentStateHistorys), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentStateHistoryVM()
        {
            var validId = Guid.NewGuid();

            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateHistoryId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                EquipmentName = "equipamento",
                Status = EquipmentStateType.Operando,
                Date = DateTime.Now,
                Equipment = new Equipment(),
            };

            var equipmentStateHistoryVM = new EquipmentStateHistoryVM
            {
                EquipmentStatehistoryId = equipmentStateHistory.EquipmentStateHistoryId,
                EquipmentId = equipmentStateHistory.EquipmentId,
                EquipmentName = equipmentStateHistory.EquipmentName,
                Status = equipmentStateHistory.Status,
                Date = equipmentStateHistory.Date
            };

            _equipmentRMock.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentStateHistory);

            _mapperMock.Setup(m => m.Map<EquipmentStateHistoryVM>(equipmentStateHistory))
                .Returns(equipmentStateHistoryVM);

            var result = await _service.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistoryVM>(result);
            Assert.Equal(equipmentStateHistoryVM.EquipmentStatehistoryId, result.EquipmentStatehistoryId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentName, result.EquipmentName);
            Assert.Equal(equipmentStateHistoryVM.Status, result.Status);
            Assert.Equal(equipmentStateHistoryVM.Date, result.Date);

            _equipmentRMock.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateHistoryVM>(equipmentStateHistory), Times.Once);
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
            var equipmentStateHistory = new EquipmentStateHistory
            {
                EquipmentStateHistoryId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                EquipmentName = "equipamento",
                Status = EquipmentStateType.Operando,
                Date = DateTime.Now,
                Equipment = new Equipment(),
            };

            var equipmentStateHistoryIM = new EquipmentStateHistoryIM
            {
                EquipmentId = equipmentStateHistory.EquipmentId,
                Status = EquipmentStateType.Operando,
                Date = equipmentStateHistory.Date
            };

            _validatorMock.Setup(v => v.Validate(equipmentStateHistoryIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentStateHistory>(equipmentStateHistoryIM))
                .Returns(equipmentStateHistory);

            _equipmentRMock.Setup(repo => repo.UpdateAsync(equipmentStateHistory))
                .Returns(Task.CompletedTask);

            await _service.UpdateAsync(equipmentStateHistory.EquipmentId, equipmentStateHistoryIM);

            _equipmentRMock.Verify(repo => repo.UpdateAsync(equipmentStateHistory), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentStateHistoryIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateHistory>(equipmentStateHistoryIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidEntity_ThrowsValidationException()
        {
            EquipmentStateHistoryIM invalidEquipmentStateHistoryIM = new EquipmentStateHistoryIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentId", "EquipmentId cannot be empty."),
                new ValidationFailure("EquipmentStateId", "EquipmentStateId cannot be empty."),
                new ValidationFailure("Date", "Date is required.")
            };

            _validatorMock.Setup(v => v.Validate(invalidEquipmentStateHistoryIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentStateHistoryIM));
            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(invalidEquipmentStateHistoryIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEntity_ThrowsArgumentNullException()
        {
            EquipmentStateHistoryIM invalidEquipmentStateHistoryIM = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentStateHistoryIM));
        }
    }
}

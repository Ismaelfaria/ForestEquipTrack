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
    public class EquipmentStateHistoryST
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentStateHistoryIM>> _validatorMock;
        private readonly Mock<IEquipmentStateHistoryR> _equipmentRMock;
        private readonly EquipmentStateHistoryS _service;

        public EquipmentStateHistoryST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentStateHistoryIM>>();
            _equipmentRMock = new Mock<IEquipmentStateHistoryR>();

            _service = new EquipmentStateHistoryS(_equipmentRMock.Object, _mapperMock.Object, _validatorMock.Object);
        }


        [Fact]
        public async void CreateAsync_ValidEquipment_ReturnsCreatedEquipmentStateHistoryVM()
        {

            var equipmentStateHistoryIM = new EquipmentStateHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            var equipmentStateHistoryEntity = new EquipmentStateHistory
            {
                EquipmentStateHistoryId = Guid.NewGuid(),
                EquipmentId = equipmentStateHistoryIM.EquipmentId,
                EquipmentStateId = equipmentStateHistoryIM.EquipmentStateId,
                Date = equipmentStateHistoryIM.Date,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            var equipmentStateHistoryVM = new EquipmentStateHistoryVM
            {
                EquipmentStatehistoryId = equipmentStateHistoryEntity.EquipmentStateHistoryId,
                EquipmentStateId = equipmentStateHistoryEntity.EquipmentStateId,
                EquipmentId = equipmentStateHistoryEntity.EquipmentId,
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

            var result = await _service.CreateAsync(equipmentStateHistoryIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentStateHistoryVM>(result);
            Assert.Equal(equipmentStateHistoryVM.EquipmentStatehistoryId, result.EquipmentStatehistoryId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentStateHistoryVM.Date, result.Date);

            _equipmentRMock.Verify(repo => repo.CreateAsync(equipmentStateHistoryEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateHistory>(equipmentStateHistoryIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentStateHistoryVM>(equipmentStateHistoryEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentStateHistoryIM), Times.Once);
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
                    EquipmentStateId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = new DateTime(2023, 1, 1, 15, 30, 0),
                    Equipment = new Equipment(),
                    EquipmentState = new EquipmentState()
                },
                new EquipmentStateHistory
                {
                    EquipmentStateHistoryId = Guid.NewGuid(),
                    EquipmentStateId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = new DateTime(2024, 1, 1, 15, 30, 0),
                    Equipment = new Equipment(),
                    EquipmentState = new EquipmentState()
                }
            };

            var equipmentStateHistoryVMs = new List<EquipmentStateHistoryVM>
            {
                new EquipmentStateHistoryVM
                {
                    EquipmentStatehistoryId = equipmentStateHistorys[0].EquipmentStateHistoryId,
                    EquipmentStateId = equipmentStateHistorys[0].EquipmentStateId,
                    EquipmentId = equipmentStateHistorys[0].EquipmentId,
                    Date = equipmentStateHistorys[0].Date
                },
                new EquipmentStateHistoryVM
                {
                    EquipmentStatehistoryId = equipmentStateHistorys[1].EquipmentStateHistoryId,
                    EquipmentStateId = equipmentStateHistorys[1].EquipmentStateId,
                    EquipmentId = equipmentStateHistorys[1].EquipmentId,
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
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            var equipmentStateHistoryVM = new EquipmentStateHistoryVM
            {
                EquipmentStatehistoryId = equipmentStateHistory.EquipmentStateHistoryId,
                EquipmentStateId = equipmentStateHistory.EquipmentStateId,
                EquipmentId = equipmentStateHistory.EquipmentId,
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
            Assert.Equal(equipmentStateHistoryVM.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentStateHistoryVM.EquipmentId, result.EquipmentId);
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
                EquipmentStateId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Equipment = new Equipment(),
                EquipmentState = new EquipmentState()
            };

            var equipmentStateHistoryIM = new EquipmentStateHistoryIM
            {
                EquipmentId = equipmentStateHistory.EquipmentId,
                EquipmentStateId = equipmentStateHistory.EquipmentStateId,
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

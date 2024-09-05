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
    public class EquipmentPositionHistoryST
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EquipmentPositionHistoryIM>> _validatorMock;
        private readonly Mock<IEquipmentPositionHistoryR> _equipmentPositionHistoryRMock;
        private readonly EquipmentPositionHistoryS _service;

        public EquipmentPositionHistoryST()
        {
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<EquipmentPositionHistoryIM>>();
            _equipmentPositionHistoryRMock = new Mock<IEquipmentPositionHistoryR>();

            _service = new EquipmentPositionHistoryS(_equipmentPositionHistoryRMock.Object, _mapperMock.Object, _validatorMock.Object);
        }


        [Fact]
        public async void CreateAsync_ValidEquipmentPositionHistory_ReturnsCreatedEquipmentPositionHistoryVM()
        {

            var equipmentPositionHistoryIM = new EquipmentPositionHistoryIM
            {
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 1234,
                Lon = 5678
            };

            var equipmentPositionHistoryEntity = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = equipmentPositionHistoryIM.EquipmentId,
                Date = equipmentPositionHistoryIM.Date,
                Lat = equipmentPositionHistoryIM.Lat,
                Lon = equipmentPositionHistoryIM.Lon,
                Equipment = new Equipment()
            };

            var equipmentPositionHistoryVM = new EquipmentPositionHistoryVM
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = equipmentPositionHistoryEntity.EquipmentId.Value,
                Date = equipmentPositionHistoryEntity.Date,
                Lat = equipmentPositionHistoryEntity.Lat,
                Lon = equipmentPositionHistoryEntity.Lon
            };


            _validatorMock.Setup(v => v.Validate(equipmentPositionHistoryIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentPositionHistory>(equipmentPositionHistoryIM))
                .Returns(equipmentPositionHistoryEntity);

            _mapperMock.Setup(m => m.Map<EquipmentPositionHistoryVM>(equipmentPositionHistoryEntity))
                .Returns(equipmentPositionHistoryVM);

            _equipmentPositionHistoryRMock.Setup(repo => repo.CreateAsync(equipmentPositionHistoryEntity))
                .ReturnsAsync(equipmentPositionHistoryEntity);

            var result = await _service.CreateAsync(equipmentPositionHistoryIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentPositionHistoryVM>(result);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentPositionId, result.EquipmentPositionId);
            Assert.Equal(equipmentPositionHistoryVM.Date, result.Date);
            Assert.Equal(equipmentPositionHistoryVM.Lon, result.Lon);
            Assert.Equal(equipmentPositionHistoryVM.Lat, result.Lat);

            _equipmentPositionHistoryRMock.Verify(repo => repo.CreateAsync(equipmentPositionHistoryEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentPositionHistory>(equipmentPositionHistoryIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentPositionHistoryVM>(equipmentPositionHistoryEntity), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentPositionHistoryIM), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_InvalidEntity_ThrowsValidationException()
        {
            var equipmentPositionHistoryIM = new EquipmentPositionHistoryIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentId", "EquipmentId cannot be empty."),
                new ValidationFailure("Date", "Date is required."),
                new ValidationFailure("Lon", "Lon is required."),
                new ValidationFailure("Lat", "Lat is required.")
            };

            _validatorMock.Setup(v => v.Validate(equipmentPositionHistoryIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(equipmentPositionHistoryIM));

            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(equipmentPositionHistoryIM), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ExceptionEquipmentPositionHistoryNull_ThrowsArgumentNullException()
        {

            EquipmentPositionHistoryIM equipmentPositionHistoryIM = null;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(equipmentPositionHistoryIM));
            Assert.Equal("Value cannot be null. (Parameter 'Entity Invalid.')", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var validId = Guid.NewGuid();

            _equipmentPositionHistoryRMock.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            await _service.DeleteAsync(validId);

            _equipmentPositionHistoryRMock.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {

            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentPositionHistoryVMs()
        {
            var equipmentPositionHistorys = new List<EquipmentPositionHistory>
            {
                new EquipmentPositionHistory
                {
                    EquipmentPositionId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Lat = 1234,
                    Lon = 5678,
                    Equipment = new Equipment()
                },
                new EquipmentPositionHistory
                {
                    EquipmentPositionId = Guid.NewGuid(),
                    EquipmentId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Lat = 9876,
                    Lon = 5432,
                    Equipment = new Equipment()
                }
            };

            var equipmentPositionHistoryVMs = new List<EquipmentPositionHistoryVM>
            {
                new EquipmentPositionHistoryVM
                {
                    EquipmentId = equipmentPositionHistorys[0].EquipmentId.Value,
                    EquipmentPositionId = equipmentPositionHistorys[0].EquipmentPositionId,
                    Date = equipmentPositionHistorys[0].Date,
                    Lon = equipmentPositionHistorys[0].Lon,
                    Lat = equipmentPositionHistorys[0].Lat
                },
                new EquipmentPositionHistoryVM
                {
                    EquipmentId = equipmentPositionHistorys[1].EquipmentId.Value,
                    EquipmentPositionId = equipmentPositionHistorys[1].EquipmentPositionId,
                    Date = equipmentPositionHistorys[1].Date,
                    Lon = equipmentPositionHistorys[1].Lon,
                    Lat = equipmentPositionHistorys[1].Lat
                }
            };

            _equipmentPositionHistoryRMock.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentPositionHistorys);

            _mapperMock.Setup(m => m.Map<IEnumerable<EquipmentPositionHistoryVM>>(equipmentPositionHistorys))
                .Returns(equipmentPositionHistoryVMs);

            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentPositionHistoryVM>>(result);
            Assert.Equal(equipmentPositionHistoryVMs.Count, result.Count());
            Assert.Contains(result, em => em.Lon == 5678);
            Assert.Contains(result, em => em.Lon == 5432);

            _equipmentPositionHistoryRMock.Verify(repo => repo.FindAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EquipmentPositionHistoryVM>>(equipmentPositionHistorys), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentPositionHistoryVM()
        {
            var validId = Guid.NewGuid();

            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 9876,
                Lon = 5432,
                Equipment = new Equipment()
            };

            var equipmentPositionHistoryVM = new EquipmentPositionHistoryVM
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = equipmentPositionHistory.EquipmentId.Value,
                Date = equipmentPositionHistory.Date,
                Lat = equipmentPositionHistory.Lat,
                Lon = equipmentPositionHistory.Lon
            };

            _equipmentPositionHistoryRMock.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentPositionHistory);

            _mapperMock.Setup(m => m.Map<EquipmentPositionHistoryVM>(equipmentPositionHistory))
                .Returns(equipmentPositionHistoryVM);

            var result = await _service.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentPositionHistoryVM>(result);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentId, result.EquipmentId);
            Assert.Equal(equipmentPositionHistoryVM.EquipmentPositionId, result.EquipmentPositionId);
            Assert.Equal(equipmentPositionHistoryVM.Date, result.Date);
            Assert.Equal(equipmentPositionHistoryVM.Lon, result.Lon);
            Assert.Equal(equipmentPositionHistoryVM.Lat, result.Lat);

            _equipmentPositionHistoryRMock.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentPositionHistoryVM>(equipmentPositionHistory), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var invalidId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentPositionHistory_CallsUpdateAsyncInRepository()
        {
            var equipmentPositionHistory = new EquipmentPositionHistory
            {
                EquipmentPositionId = Guid.NewGuid(),
                EquipmentId = Guid.NewGuid(),
                Date = DateTime.Now,
                Lat = 9876,
                Lon = 5432,
                Equipment = new Equipment()
            };

            var equipmentPositionHistoryIM = new EquipmentPositionHistoryIM
            {
                EquipmentId = equipmentPositionHistory.EquipmentId,
                Date = equipmentPositionHistory.Date,
                Lon = equipmentPositionHistory.Lon,
                Lat = equipmentPositionHistory.Lat
            };

            _validatorMock.Setup(v => v.Validate(equipmentPositionHistoryIM))
                .Returns(new ValidationResult());

            _mapperMock.Setup(m => m.Map<EquipmentPositionHistory>(equipmentPositionHistoryIM))
                .Returns(equipmentPositionHistory);

            _equipmentPositionHistoryRMock.Setup(repo => repo.UpdateAsync(equipmentPositionHistory))
                .Returns(Task.CompletedTask);

            await _service.UpdateAsync(equipmentPositionHistory.EquipmentPositionId, equipmentPositionHistoryIM);

            _equipmentPositionHistoryRMock.Verify(repo => repo.UpdateAsync(equipmentPositionHistory), Times.Once);
            _validatorMock.Verify(v => v.Validate(equipmentPositionHistoryIM), Times.Once);
            _mapperMock.Verify(m => m.Map<EquipmentPositionHistory>(equipmentPositionHistoryIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidEntity_ThrowsValidationException()
        {
            EquipmentPositionHistoryIM invalidEquipmentPositionHistoryIM = new EquipmentPositionHistoryIM();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("EquipmentId", "EquipmentId cannot be empty."),
                new ValidationFailure("Date", "Date is required."),
                new ValidationFailure("Lon", "Lon is required."),
                new ValidationFailure("Lat", "Lat is required.")
            };

            _validatorMock.Setup(v => v.Validate(invalidEquipmentPositionHistoryIM))
                         .Returns(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentPositionHistoryIM));
            Assert.Contains("Validation failed", exception.Message);

            foreach (var failure in validationFailures)
            {
                Assert.Contains(failure.ErrorMessage, exception.Message);
            }

            _validatorMock.Verify(v => v.Validate(invalidEquipmentPositionHistoryIM), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEntity_ThrowsArgumentNullException()
        {
            EquipmentPositionHistoryIM invalidEquipmentPositionHistoryIM = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(Guid.NewGuid(), invalidEquipmentPositionHistoryIM));
        }
    }
}

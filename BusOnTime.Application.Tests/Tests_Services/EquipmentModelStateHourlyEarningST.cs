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
    public class EquipmentModelStateHourlyEarningST
    {
        [Fact]
        public async Task CreateAsync_ValidEquipmentModelStateHourlyEarning_ReturnsCreatedEquipmentModelStateHourlyEarningVM()
        {
            var equipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            var mapperMock = new Mock<IMapper>();

            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10
            };

            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
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

            validatorMock.Setup(v => v.Validate(equipmentModelStateHourlyEarningsIM))
                .Returns(new ValidationResult());

            mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM))
                .Returns(equipmentModelStateHourlyEarnings);

            mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarnings))
                .Returns(equipmentModelStateHourlyEarningsVM);

            equipmentModelStateHourlyEarningsRepository.Setup(repo => repo.CreateAsync(equipmentModelStateHourlyEarnings))
                .ReturnsAsync(equipmentModelStateHourlyEarnings);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(
                equipmentModelStateHourlyEarningsRepository.Object,
                mapperMock.Object,
                validatorMock.Object);

            var result = await equipmentModelStateHourlyEarningService.CreateAsync(equipmentModelStateHourlyEarningsIM);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarningsVM>(result);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentModelStateHourlyEarningsVM.Value, result.Value);

            equipmentModelStateHourlyEarningsRepository.Verify(repo => repo.CreateAsync(equipmentModelStateHourlyEarnings), Times.Once);
            validatorMock.Verify(v => v.Validate(equipmentModelStateHourlyEarningsIM), Times.Once);
            mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM), Times.Once);
            mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarnings), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            var mapperMock = new Mock<IMapper>();
            var validId = Guid.NewGuid();

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(
                mockEquipmentModelStateHourlyEarningsRepository.Object,
                mapperMock.Object,
                validatorMock.Object);

            await equipmentModelStateHourlyEarningService.DeleteAsync(validId);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            var mapperMock = new Mock<IMapper>();
            var invalidId = Guid.Empty;

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(
                mockEquipmentModelStateHourlyEarningsRepository.Object,
                mapperMock.Object,
                validatorMock.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelStateHourlyEarningService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentModelStateHourlyEarningsVM()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            var mapperMock = new Mock<IMapper>();
            var equipmentModelStateHourlyEarnings = new List<EquipmentModelStateHourlyEarnings>
            {
                new EquipmentModelStateHourlyEarnings
                {
                    EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    EquipmentStateId = Guid.NewGuid(),
                    Value = 10,
                    EquipmentModel = new EquipmentModel(),
                    EquipmentState = new EquipmentState()
                },
                new EquipmentModelStateHourlyEarnings
                {
                    EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                    EquipmentModelId = Guid.NewGuid(),
                    EquipmentStateId = Guid.NewGuid(),
                    Value = 11,
                    EquipmentModel = new EquipmentModel(),
                    EquipmentState = new EquipmentState()
                }
            };

            var equipmentModelStateHourlyEarningsVMs = equipmentModelStateHourlyEarnings.Select(e => new EquipmentModelStateHourlyEarningsVM
            {
                EquipmentModelStateHourlyEarningsId = e.EquipmentModelStateHourlyEarningsId,
                EquipmentModelId = e.EquipmentModelId,
                EquipmentStateId = e.EquipmentStateId,
                Value = e.Value
            }).ToList();

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(equipmentModelStateHourlyEarnings);

            mapperMock.Setup(m => m.Map<IEnumerable<EquipmentModelStateHourlyEarningsVM>>(equipmentModelStateHourlyEarnings))
                .Returns(equipmentModelStateHourlyEarningsVMs);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(
                mockEquipmentModelStateHourlyEarningsRepository.Object,
                mapperMock.Object,
                validatorMock.Object);

            var result = await equipmentModelStateHourlyEarningService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModelStateHourlyEarningsVM>>(result);
            Assert.Equal(equipmentModelStateHourlyEarningsVMs.Count, result.Count());
            Assert.Contains(result, em => em.Value == 10);
            Assert.Contains(result, em => em.Value == 11);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentModelStateHourlyEarning_ReturnsUpdatedEquipmentModelStateHourlyEarningVM()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            var mapperMock = new Mock<IMapper>();
            var id = Guid.NewGuid();

            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 15
            };

            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = id,
                EquipmentModelId = equipmentModelStateHourlyEarningsIM.EquipmentModelId,
                EquipmentStateId = equipmentModelStateHourlyEarningsIM.EquipmentStateId,
                Value = equipmentModelStateHourlyEarningsIM.Value
            };

            var equipmentModelStateHourlyEarningsVM = new EquipmentModelStateHourlyEarningsVM
            {
                EquipmentModelStateHourlyEarningsId = id,
                EquipmentModelId = equipmentModelStateHourlyEarningsIM.EquipmentModelId,
                EquipmentStateId = equipmentModelStateHourlyEarningsIM.EquipmentStateId,
                Value = equipmentModelStateHourlyEarningsIM.Value
            };

            validatorMock.Setup(v => v.Validate(equipmentModelStateHourlyEarningsIM))
                .Returns(new ValidationResult());

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.GetByIdAsync(equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId))
                .ReturnsAsync(equipmentModelStateHourlyEarnings);

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings))
                .Returns(Task.CompletedTask);

            mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM))
                .Returns(equipmentModelStateHourlyEarnings);

            mapperMock.Setup(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarnings))
                .Returns(equipmentModelStateHourlyEarningsVM);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(
                mockEquipmentModelStateHourlyEarningsRepository.Object,
                mapperMock.Object,
                validatorMock.Object);

            await equipmentModelStateHourlyEarningService.UpdateAsync(id, equipmentModelStateHourlyEarningsIM);      

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings), Times.Once);
            validatorMock.Verify(v => v.Validate(equipmentModelStateHourlyEarningsIM), Times.Once);
            mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarnings>(equipmentModelStateHourlyEarningsIM), Times.Once);
            mapperMock.Verify(m => m.Map<EquipmentModelStateHourlyEarningsVM>(equipmentModelStateHourlyEarnings), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validatorMock = new Mock<IValidator<EquipmentModelStateHourlyEarningsIM>>();
            var mapperMock = new Mock<IMapper>();
            var invalidId = Guid.Empty;

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(
                mockEquipmentModelStateHourlyEarningsRepository.Object,
                mapperMock.Object,
                validatorMock.Object);

            var equipmentModelStateHourlyEarningsIM = new EquipmentModelStateHourlyEarningsIM
            {
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentModelStateHourlyEarningService.UpdateAsync(invalidId, null));
        }
    }
}

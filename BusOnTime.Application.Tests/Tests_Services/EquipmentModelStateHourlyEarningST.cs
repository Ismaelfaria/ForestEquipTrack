using BusOnTime.Application.Services;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
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
        public async void CreateAsync_ValidEquipmentModelStateHourlyEarning_ReturnsCreatedEquipmentModelStateHourlyEarning()
        {
            var equipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();

            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            equipmentModelStateHourlyEarningsRepository.Setup(repo => repo.CreateAsync(equipmentModelStateHourlyEarnings))
            .ReturnsAsync(equipmentModelStateHourlyEarnings);


            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(equipmentModelStateHourlyEarningsRepository.Object);

            var result = await equipmentModelStateHourlyEarningService.CreateAsync(equipmentModelStateHourlyEarnings);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarnings>(result);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentModelStateHourlyEarnings.Value, result.Value);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentState, result.EquipmentState);

            equipmentModelStateHourlyEarningsRepository.Verify(repo => repo.CreateAsync(equipmentModelStateHourlyEarnings), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteAsyncInRepository()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validId = Guid.NewGuid();

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            await equipmentModelStateHourlyEarningService.DeleteAsync(validId);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.DeleteAsync(validId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var invalidId = Guid.Empty;

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelStateHourlyEarningService.DeleteAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task FindAllAsync_ReturnsListOfEquipmentModelStateHourlyEarnings()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
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

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.FindAllAsync())
            .ReturnsAsync(equipmentModelStateHourlyEarnings);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var result = await equipmentModelStateHourlyEarningService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<EquipmentModelStateHourlyEarnings>>(result);
            Assert.Equal(equipmentModelStateHourlyEarnings.Count, result.Count());
            Assert.Contains(result, em => em.Value == 10);
            Assert.Contains(result, em => em.Value == 11);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEquipmentModelStateHourlyEarnings()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var validId = Guid.NewGuid();
            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(equipmentModelStateHourlyEarnings);

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var result = await equipmentModelStateHourlyEarningService.GetByIdAsync(validId);

            Assert.NotNull(result);
            Assert.IsType<EquipmentModelStateHourlyEarnings>(result);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelStateHourlyEarningsId, result.EquipmentModelStateHourlyEarningsId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModelId, result.EquipmentModelId);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentStateId, result.EquipmentStateId);
            Assert.Equal(equipmentModelStateHourlyEarnings.Value, result.Value);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentModel, result.EquipmentModel);
            Assert.Equal(equipmentModelStateHourlyEarnings.EquipmentState, result.EquipmentState);


            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var invalidId = Guid.Empty;

            var equipmentModelStateHourlyEarningService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => equipmentModelStateHourlyEarningService.GetByIdAsync(invalidId));
            Assert.Equal("Invalid ID.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidEquipmentModel_CallsUpdateAsyncInRepository()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            var equipmentModelStateHourlyEarnings = new EquipmentModelStateHourlyEarnings
            {
                EquipmentModelStateHourlyEarningsId = Guid.NewGuid(),
                EquipmentModelId = Guid.NewGuid(),
                EquipmentStateId = Guid.NewGuid(),
                Value = 10,
                EquipmentModel = new EquipmentModel(),
                EquipmentState = new EquipmentState()
            };

            mockEquipmentModelStateHourlyEarningsRepository.Setup(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings))
                .Returns(Task.CompletedTask);

            var equipmentModelStateHourlyEarningsService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            await equipmentModelStateHourlyEarningsService.UpdateAsync(equipmentModelStateHourlyEarnings);

            mockEquipmentModelStateHourlyEarningsRepository.Verify(repo => repo.UpdateAsync(equipmentModelStateHourlyEarnings), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NullEquipmentModel_ThrowsArgumentNullException()
        {
            var mockEquipmentModelStateHourlyEarningsRepository = new Mock<IEquipmentModelStateHourlyEarningsR>();
            EquipmentModelStateHourlyEarnings? nullEquipmentModelStateHourlyEarnings = null;

            var equipmentModelStateHourlyEarningsService = new EquipmentModelStateHourlyEarningS(mockEquipmentModelStateHourlyEarningsRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => equipmentModelStateHourlyEarningsService.UpdateAsync(nullEquipmentModelStateHourlyEarnings));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

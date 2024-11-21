using Moq;
using Xunit;
using FluentAssertions;
using SIMECAPI.Controllers;
using SIMECAPI.Repositories;
using SIMECAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIMECAPI.Tests
{
    public class ConsumoEnergiaControllerTests
    {
        private readonly Mock<IConsumoEnergiaRepository> _consumoEnergiaRepositoryMock;
        private readonly ConsumoEnergiaController _consumoEnergiaController;

        public ConsumoEnergiaControllerTests()
        {
            _consumoEnergiaRepositoryMock = new Mock<IConsumoEnergiaRepository>();
            _consumoEnergiaController = new ConsumoEnergiaController(_consumoEnergiaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllConsumos_ShouldReturnOk_WithListOfConsumos()
        {
            // Arrange
            var consumos = new List<ConsumoEnergia>
            {
                new ConsumoEnergia { IdConsumo = 1, ConsumoKwh = 100, DataLeitura = DateTime.Now, IdApartamento = 1 },
                new ConsumoEnergia { IdConsumo = 2, ConsumoKwh = 200, DataLeitura = DateTime.Now, IdApartamento = 2 }
            };
            _consumoEnergiaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(consumos);

            // Act
            var result = await _consumoEnergiaController.GetAllConsumos();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(consumos);
        }

        [Fact]
        public async Task GetConsumoById_ShouldReturnNotFound_WhenConsumoDoesNotExist()
        {
            // Arrange
            _consumoEnergiaRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ConsumoEnergia)null);

            // Act
            var result = await _consumoEnergiaController.GetConsumoById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetConsumoById_ShouldReturnOk_WithConsumo()
        {
            // Arrange
            var consumo = new ConsumoEnergia { IdConsumo = 1, ConsumoKwh = 100, DataLeitura = DateTime.Now, IdApartamento = 1 };
            _consumoEnergiaRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(consumo);

            // Act
            var result = await _consumoEnergiaController.GetConsumoById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(consumo);
        }
    }
}

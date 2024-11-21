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
    public class MoedaControllerTests
    {
        private readonly Mock<IMoedaRepository> _moedaRepositoryMock;
        private readonly MoedaController _moedaController;

        public MoedaControllerTests()
        {
            _moedaRepositoryMock = new Mock<IMoedaRepository>();
            _moedaController = new MoedaController(_moedaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllMoedas_ShouldReturnOk_WithListOfMoedas()
        {
            // Arrange
            var moedas = new List<Moeda>
            {
                new Moeda { IdMoeda = 1, Quantidade = 10, IdUsuario = 1 },
                new Moeda { IdMoeda = 2, Quantidade = 20, IdUsuario = 2 }
            };
            _moedaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(moedas);

            // Act
            var result = await _moedaController.GetAllMoedas();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(moedas);
        }

        [Fact]
        public async Task GetMoedaById_ShouldReturnNotFound_WhenMoedaDoesNotExist()
        {
            // Arrange
            _moedaRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Moeda)null);

            // Act
            var result = await _moedaController.GetMoedaById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetMoedaById_ShouldReturnOk_WithMoeda()
        {
            // Arrange
            var moeda = new Moeda { IdMoeda = 1, Quantidade = 10, IdUsuario = 1 };
            _moedaRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(moeda);

            // Act
            var result = await _moedaController.GetMoedaById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(moeda);
        }
    }
}

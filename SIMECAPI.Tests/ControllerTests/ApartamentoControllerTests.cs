using Moq;
using Xunit;
using FluentAssertions;
using SIMECAPI.Controllers;
using SIMECAPI.Repositories;
using SIMECAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIMECAPI.Tests.ControllerTests
{
    public class ApartamentoControllerTests
    {
        private readonly Mock<IApartamentoRepository> _apartamentoRepositoryMock;
        private readonly ApartamentoController _apartamentoController;

        public ApartamentoControllerTests()
        {
            _apartamentoRepositoryMock = new Mock<IApartamentoRepository>();
            _apartamentoController = new ApartamentoController(_apartamentoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllApartamentos_ShouldReturnOk_WithListOfApartamentos()
        {
            // Arrange
            var apartamentos = new List<Apartamento>
            {
                new Apartamento { IdApartamento = 1, Numero = "101", Andar = 1, IdUsuario = 1, IdDica = 1 },
                new Apartamento { IdApartamento = 2, Numero = "102", Andar = 2, IdUsuario = 2, IdDica = 2 }
            };
            _apartamentoRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(apartamentos);

            // Act
            var result = await _apartamentoController.GetAllApartamentos();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(apartamentos);
        }

        [Fact]
        public async Task GetApartamentoById_ShouldReturnNotFound_WhenApartamentoDoesNotExist()
        {
            // Arrange
            _apartamentoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Apartamento)null);

            // Act
            var result = await _apartamentoController.GetApartamentoById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetApartamentoById_ShouldReturnOk_WithApartamento()
        {
            // Arrange
            var apartamento = new Apartamento { IdApartamento = 1, Numero = "101", Andar = 1, IdUsuario = 1, IdDica = 1 };
            _apartamentoRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(apartamento);

            // Act
            var result = await _apartamentoController.GetApartamentoById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(apartamento);
        }
    }
}

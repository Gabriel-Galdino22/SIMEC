using Xunit;
using FluentAssertions;
using SIMECAPI.Controllers;
using SIMECAPI.Repositories;
using SIMECAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;

namespace SIMECAPI.Tests
{
    public class DicaSustentabilidadeControllerTests
    {
        private readonly Mock<IDicaSustentabilidadeRepository> _dicaSustentabilidadeRepositoryMock;
        private readonly DicaSustentabilidadeController _dicaSustentabilidadeController;

        public DicaSustentabilidadeControllerTests()
        {
            _dicaSustentabilidadeRepositoryMock = new Mock<IDicaSustentabilidadeRepository>();
            _dicaSustentabilidadeController = new DicaSustentabilidadeController(_dicaSustentabilidadeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetDicaById_ShouldReturnOk_WithDica()
        {
            // Arrange
            var dica = new DicaSustentabilidade { IdDica = 1, Titulo = "Teste", Descricao = "Descrição Teste", DataCriacao = System.DateTime.Now };
            _dicaSustentabilidadeRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(dica);

            // Act
            var result = await _dicaSustentabilidadeController.GetDicaById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(dica);
        }

        [Fact]
        public async Task GetDicaById_ShouldReturnNotFound_WhenDicaDoesNotExist()
        {
            // Arrange
            _dicaSustentabilidadeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((DicaSustentabilidade)null);

            // Act
            var result = await _dicaSustentabilidadeController.GetDicaById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}

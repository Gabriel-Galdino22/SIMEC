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
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly UsuarioController _usuarioController;

        public UsuarioControllerTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _usuarioController = new UsuarioController(_usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllUsuarios_ShouldReturnOk_WithListOfUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { IdUsuario = 1, Nome = "João", Email = "joao@example.com", Senha = "123456" },
                new Usuario { IdUsuario = 2, Nome = "Maria", Email = "maria@example.com", Senha = "654321" }
            };
            _usuarioRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(usuarios);

            var result = await _usuarioController.GetAllUsuarios();

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(usuarios);
        }

        [Fact]
        public async Task GetUsuarioById_ShouldReturnNotFound_WhenUsuarioDoesNotExist()
        {
            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Usuario)null);

            var result = await _usuarioController.GetUsuarioById(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetUsuarioById_ShouldReturnOk_WithUsuario()
        {
            var usuario = new Usuario { IdUsuario = 1, Nome = "João", Email = "joao@example.com", Senha = "123456" };
            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(usuario);

            var result = await _usuarioController.GetUsuarioById(1);

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(usuario);
        }
    }
}

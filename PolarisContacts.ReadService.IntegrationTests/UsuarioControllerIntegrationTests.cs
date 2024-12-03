using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;

namespace PolarisContacts.IntegrationTests
{
    public class UsuarioControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly IUsuarioService _usuarioServiceMock;

        public UsuarioControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _usuarioServiceMock = Substitute.For<IUsuarioService>();

            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Substituindo o IUsuarioService pelo mock
                    services.AddSingleton(_usuarioServiceMock);
                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetUserByPasswordAsync_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var login = "validUser";
            var senha = "validPassword";
            var expectedUser = new Usuario { Id = 1, Login = login, Senha = senha };

            _usuarioServiceMock.GetUserByPasswordAsync(login, senha).Returns(expectedUser);

            // Act
            var response = await _client.GetAsync($"/Read/Usuario/GetUserByPasswordAsync?login={login}&senha={senha}");

            // Assert
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<Usuario>();
            Assert.NotNull(user);
            Assert.Equal(expectedUser.Id, user.Id);
        }

        [Fact]
        public async Task GetUserByPasswordAsync_InvalidLogin_ReturnsBadRequest()
        {
            // Arrange
            var login = ""; // Login inválido
            var senha = "somePassword";

            // Act
            var response = await _client.GetAsync($"/Read/Usuario/GetUserByPasswordAsync?login={login}&senha={senha}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetUserByPasswordAsync_InvalidPassword_ReturnsBadRequest()
        {
            // Arrange
            var login = "someUser";
            var senha = ""; // Senha inválida

            // Act
            var response = await _client.GetAsync($"/Read/Usuario/GetUserByPasswordAsync?login={login}&senha={senha}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetUserByPasswordAsync_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var login = "invalidUser";
            var senha = "invalidPassword";

            _usuarioServiceMock.GetUserByPasswordAsync(login, senha).Returns((Usuario)null); // Simula não encontrado

            // Act
            var response = await _client.GetAsync($"/Read/Usuario/GetUserByPasswordAsync?login={login}&senha={senha}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

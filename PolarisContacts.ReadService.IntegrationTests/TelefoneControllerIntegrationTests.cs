using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;

namespace PolarisContacts.IntegrationTests
{
    public class TelefoneControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ITelefoneService _telefoneServiceMock;

        public TelefoneControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _telefoneServiceMock = Substitute.For<ITelefoneService>();

            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_telefoneServiceMock);
                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetTelefonesByIdContato_ValidIdContato_ReturnsTelefones()
        {
            // Arrange
            int validIdContato = 50;
            var telefones = new List<Telefone>
            {
                new Telefone { Id = 1, IdContato = validIdContato, IdRegiao = 1, NumeroTelefone = "11987654321", Ativo = true },
                new Telefone { Id = 2, IdContato = validIdContato, IdRegiao = 1, NumeroTelefone = "11912345678", Ativo = true }
            };

            _telefoneServiceMock.GetTelefonesByIdContato(validIdContato).Returns(telefones);

            // Act
            var response = await _client.GetAsync($"/Read/Telefone/GetTelefonesByIdContato/{validIdContato}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultadoTelefones = await response.Content.ReadFromJsonAsync<IEnumerable<Telefone>>();
            Assert.NotNull(resultadoTelefones);
            Assert.NotEmpty(resultadoTelefones);
        }
        

        [Fact]
        public async Task GetTelefoneById_ValidId_ReturnsTelefone()
        {
            // Arrange
            int validId = 1;
            var expectedTelefone = new Telefone
            {
                Id = validId,
                IdContato = 50,
                IdRegiao = 1,
                NumeroTelefone = "11987654321",
                Ativo = true
            };

            _telefoneServiceMock.GetTelefoneById(validId).Returns(expectedTelefone);

            // Act
            var response = await _client.GetAsync($"/Read/Telefone/GetTelefoneById/{validId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var telefone = await response.Content.ReadFromJsonAsync<Telefone>();
            Assert.NotNull(telefone);
            Assert.Equal(validId, telefone.Id);
        }

        [Fact]
        public async Task GetTelefoneById_InvalidId_ReturnsNoContent()
        {
            // Arrange
            int invalidId = 999;
            _telefoneServiceMock.GetTelefoneById(invalidId).Returns((Telefone)null); // Retorna null para simular não encontrado

            // Act
            var response = await _client.GetAsync($"/Read/Telefone/GetTelefoneById/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using PolarisContacts.ReadService.Domain;

namespace PolarisContacts.IntegrationTests
{
    public class CelularControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ICelularService _celularServiceMock;

        public CelularControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _celularServiceMock = Substitute.For<ICelularService>();

            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Substituindo o ICelularService pelo mock
                    services.AddSingleton(_celularServiceMock);
                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetCelularesByIdContato_ValidIdContato_ReturnsCelulares()
        {
            // Arrange
            int validIdContato = 50;
            var Celulares = new List<Celular>
            {
                new Celular { Id = 1, NumeroCelular = "1234-5678", IdContato = validIdContato },
                new Celular { Id = 2, NumeroCelular     = "8765-4321", IdContato = validIdContato }
            };

            _celularServiceMock.GetCelularesByIdContato(validIdContato).Returns(Celulares);

            // Act
            var response = await _client.GetAsync($"/Celular/GetCelularesByIdContato/{validIdContato}");

            // Assert
            response.EnsureSuccessStatusCode();
            var celulares = await response.Content.ReadFromJsonAsync<IEnumerable<Celular>>();
            Assert.NotNull(celulares);
            Assert.NotEmpty(celulares);
        }

        [Fact]
        public async Task GetCelularById_ValidId_ReturnsCelular()
        {
            // Arrange
            int validId = 1;
            var expectedCelular = new Celular { Id = validId, NumeroCelular = "91234-5678", IdContato = 50 };

            _celularServiceMock.GetCelularById(validId).Returns(expectedCelular);

            // Act
            var response = await _client.GetAsync($"/Celular/GetCelularById/{validId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var celular = await response.Content.ReadFromJsonAsync<Celular>();
            Assert.NotNull(celular);
            Assert.Equal(validId, celular.Id);
        }

        [Fact]
        public async Task GetCelularById_InvalidId_ReturnsNoContent()
        {
            // Arrange
            int invalidId = 999;
            _celularServiceMock.GetCelularById(invalidId).Returns((Celular)null); // Retorna null para simular não encontrado

            // Act
            var response = await _client.GetAsync($"/Celular/GetCelularById/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
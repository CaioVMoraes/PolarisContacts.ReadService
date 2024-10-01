using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace PolarisContacts.IntegrationTests
{
    public class CelularControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CelularControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCelularesByIdContato_ValidIdContato_ReturnsCelulares()
        {
            // Arrange
            int validIdContato = 50; // Um ID de contato válido previamente carregado no banco em memória

            // Act
            var response = await _client.GetAsync($"/Celular/GetCelularesByIdContato/{validIdContato}");

            // Assert
            response.EnsureSuccessStatusCode();
            var celulares = await response.Content.ReadFromJsonAsync<IEnumerable<Celular>>();
            Assert.NotNull(celulares);
            Assert.NotEmpty(celulares);
        }

        //[Fact]
        //public async Task GetCelularesByIdContato_InvalidIdContato_ReturnsNotFound()
        //{
        //    // Arrange
        //    int invalidIdContato = 999; // Um ID de contato inexistente

        //    // Act
        //    var response = await _client.GetAsync($"/CelularController/GetCelularesByIdContato/{invalidIdContato}");

        //    // Assert
        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}

        //[Fact]
        //public async Task GetCelularById_ValidId_ReturnsCelular()
        //{
        //    // Arrange
        //    int validId = 1; // Um ID de celular válido previamente carregado no banco em memória

        //    // Act
        //    var response = await _client.GetAsync($"/CelularController/GetCelularById/{validId}");

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    var celular = await response.Content.ReadFromJsonAsync<Celular>();
        //    Assert.NotNull(celular);
        //    Assert.Equal(validId, celular.Id);
        //}

        //[Fact]
        //public async Task GetCelularById_InvalidId_ReturnsNotFound()
        //{
        //    // Arrange
        //    int invalidId = 999; // Um ID de celular inexistente

        //    // Act
        //    var response = await _client.GetAsync($"/CelularController/GetCelularById/{invalidId}");

        //    // Assert
        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}
    }
}

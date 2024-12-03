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
    public class EnderecoControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly IEnderecoService _enderecoServiceMock;

        public EnderecoControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _enderecoServiceMock = Substitute.For<IEnderecoService>();

            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_enderecoServiceMock);
                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetEnderecosByIdContato_ValidIdContato_ReturnsEnderecos()
        {
            // Arrange
            int validIdContato = 50;
            var enderecos = new List<Endereco>
            {
                new Endereco { Id = 1, Logradouro = "Rua A", Numero = "123", IdContato = validIdContato, Cidade = "Cidade A", Estado = "Estado A", CEP = "12345-678", Ativo = true },
                new Endereco { Id = 2, Logradouro = "Rua B", Numero = "456", IdContato = validIdContato, Cidade = "Cidade B", Estado = "Estado B", CEP = "23456-789", Ativo = true }
            };

            _enderecoServiceMock.GetEnderecosByIdContato(validIdContato).Returns(enderecos);

            // Act
            var response = await _client.GetAsync($"/Read/Endereco/GetEnderecosByIdContato/{validIdContato}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultadoEnderecos = await response.Content.ReadFromJsonAsync<IEnumerable<Endereco>>();
            Assert.NotNull(resultadoEnderecos);
            Assert.NotEmpty(resultadoEnderecos);
        }
      

        [Fact]
        public async Task GetEnderecoById_ValidId_ReturnsEndereco()
        {
            // Arrange
            int validId = 1;
            var expectedEndereco = new Endereco
            {
                Id = validId,
                Logradouro = "Rua A",
                Numero = "123",
                IdContato = 50,
                Cidade = "Cidade A",
                Estado = "Estado A",
                CEP = "12345-678",
                Ativo = true
            };

            _enderecoServiceMock.GetEnderecoById(validId).Returns(expectedEndereco);

            // Act
            var response = await _client.GetAsync($"/Read/Endereco/GetEnderecoById/{validId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var endereco = await response.Content.ReadFromJsonAsync<Endereco>();
            Assert.NotNull(endereco);
            Assert.Equal(validId, endereco.Id);
        }

        [Fact]
        public async Task GetEnderecoById_InvalidId_ReturnsNoContent()
        {
            // Arrange
            int invalidId = 999;
            _enderecoServiceMock.GetEnderecoById(invalidId).Returns((Endereco)null); // Retorna null para simular não encontrado

            // Act
            var response = await _client.GetAsync($"/Read/Endereco/GetEnderecoById/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

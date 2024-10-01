using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
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
    public class ContatoControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly IContatoService _contatoServiceMock;

        public ContatoControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _contatoServiceMock = Substitute.For<IContatoService>();

            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_contatoServiceMock);
                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllContatosByIdUsuario_ValidIdUsuario_ReturnsContatos()
        {
            // Arrange
            int validIdUsuario = 1;
            var contatos = new List<Contato>
            {
                new Contato { Id = 1, Nome = "Contato 1" },
                new Contato { Id = 2, Nome = "Contato 2" }
            };

            _contatoServiceMock.GetAllContatosByIdUsuario(validIdUsuario).Returns(contatos);

            // Act
            var response = await _client.GetAsync($"/Contato/GetAllContatosByIdUsuario/{validIdUsuario}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultadoContatos = await response.Content.ReadFromJsonAsync<IEnumerable<Contato>>();
            Assert.NotNull(resultadoContatos);
            Assert.Equal(2, resultadoContatos.Count());
        }
        

        [Fact]
        public async Task GetContatoById_ValidId_ReturnsContato()
        {
            // Arrange
            int validIdContato = 1;
            var expectedContato = new Contato
            {
                Id = validIdContato,
                Nome = "Contato 1",
                Telefones = new List<Telefone>
                {
                    new Telefone { Id = 1, IdContato = validIdContato, NumeroTelefone = "11987654321" }
                }
            };

            _contatoServiceMock.GetContatoById(validIdContato).Returns(expectedContato);

            // Act
            var response = await _client.GetAsync($"/Contato/GetContatoById/{validIdContato}");

            // Assert
            response.EnsureSuccessStatusCode();
            var contato = await response.Content.ReadFromJsonAsync<Contato>();
            Assert.NotNull(contato);
            Assert.Equal(validIdContato, contato.Id);
        }

        [Fact]
        public async Task GetContatoById_InvalidId_ReturnsNoContent()
        {
            // Arrange
            int invalidIdContato = 999;
            _contatoServiceMock.GetContatoById(invalidIdContato).Returns((Contato)null); // Simula não encontrado

            // Act
            var response = await _client.GetAsync($"/Contato/GetContatoById/{invalidIdContato}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task SearchContatosByIdUsuario_ValidIdUsuario_ReturnsContatos()
        {
            // Arrange
            int validIdUsuario = 1;
            string searchTerm = "Contato";
            var contatos = new List<Contato>
            {
                new Contato { Id = 1, Nome = "Contato 1" },
                new Contato { Id = 2, Nome = "Contato 2" }
            };

            _contatoServiceMock.SearchContatosByIdUsuario(validIdUsuario, searchTerm).Returns(contatos);

            // Act
            var response = await _client.GetAsync($"/Contato/SearchContatosByIdUsuario/{validIdUsuario}?searchTerm={searchTerm}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultadoContatos = await response.Content.ReadFromJsonAsync<IEnumerable<Contato>>();
            Assert.NotNull(resultadoContatos);
            Assert.Equal(2, resultadoContatos.Count());
        }

        [Fact]
        public async Task SearchContatosByIdUsuario_InvalidIdUsuario_ReturnsOK()
        {
            // Arrange
            int invalidIdUsuario = 999;
            string searchTerm = "Contato";

            _contatoServiceMock.SearchContatosByIdUsuario(invalidIdUsuario, searchTerm).Returns(new List<Contato>()); // Simula não encontrado

            // Act
            var response = await _client.GetAsync($"/Contato/SearchContatosByIdUsuario/{invalidIdUsuario}?searchTerm={searchTerm}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

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
    public class EmailControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly IEmailService _emailServiceMock;

        public EmailControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _emailServiceMock = Substitute.For<IEmailService>();

            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Substituindo o IEmailService pelo mock
                    services.AddSingleton(_emailServiceMock);
                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetEmailsByIdContato_ValidIdContato_ReturnsEmails()
        {
            // Arrange
            int validIdContato = 1;
            var emails = new List<Email>
            {
                new Email { Id = 1, IdContato = validIdContato, EnderecoEmail = "email1@example.com" },
                new Email { Id = 2, IdContato = validIdContato, EnderecoEmail = "email2@example.com" }
            };

            _emailServiceMock.GetEmailsByIdContato(validIdContato).Returns(emails);

            // Act
            var response = await _client.GetAsync($"/Read/Email/GetEmailsByIdContato/{validIdContato}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultadoEmails = await response.Content.ReadFromJsonAsync<IEnumerable<Email>>();
            Assert.NotNull(resultadoEmails);
            Assert.Equal(2, resultadoEmails.Count());
        }      

        [Fact]
        public async Task GetEmailById_ValidId_ReturnsEmail()
        {
            // Arrange
            int validId = 1;
            var expectedEmail = new Email
            {
                Id = validId,
                IdContato = 1,
                EnderecoEmail = "email1@example.com",
                Ativo = true
            };

            _emailServiceMock.GetEmailById(validId).Returns(expectedEmail);

            // Act
            var response = await _client.GetAsync($"/Read/Email/GetEmailById/{validId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var email = await response.Content.ReadFromJsonAsync<Email>();
            Assert.NotNull(email);
            Assert.Equal(validId, email.Id);
        }

        [Fact]
        public async Task GetEmailById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 999; // ID que não existe
            _emailServiceMock.GetEmailById(invalidId).Returns((Email)null); // Simula não encontrado

            // Act
            var response = await _client.GetAsync($"/Read/Email/GetEmailById/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

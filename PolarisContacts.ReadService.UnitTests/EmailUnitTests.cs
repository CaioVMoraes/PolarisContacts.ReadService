using NSubstitute;
using PolarisContacts.Domain;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.UnitTests
{
    public class EmailUnitTests
    {
        private readonly IEmailRepository _emailRepository;
        private readonly EmailService _emailService;

        public EmailUnitTests()
        {
            _emailRepository = Substitute.For<IEmailRepository>();
            _emailService = new EmailService(_emailRepository);
        }

        // Teste para GetEmailsByIdContato
        [Fact]
        public async Task GetEmailsByIdContato_ValidId_ReturnsEmails()
        {
            // Arrange
            var idContato = 1;
            var expectedEmails = new List<Email>
            {
                new Email { Id = 1, EnderecoEmail = "email1@example.com" },
                new Email { Id = 2, EnderecoEmail = "email2@example.com" }
            };

            _emailRepository.GetEmailsByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Email>>(expectedEmails));

            // Act
            var result = await _emailService.GetEmailsByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Email>)result).Count); // Verifica se foram retornados dois emails
        }

        [Fact]
        public async Task GetEmailsByIdContato_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _emailService.GetEmailsByIdContato(invalidId));
        }

        // Teste para GetEmailById
        [Fact]
        public async Task GetEmailById_ValidId_ReturnsEmail()
        {
            // Arrange
            var id = 1;
            var expectedEmail = new Email { Id = id, EnderecoEmail = "email1@example.com" };

            _emailRepository.GetEmailById(id).Returns(Task.FromResult(expectedEmail));

            // Act
            var result = await _emailService.GetEmailById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEmail.Id, result.Id);
        }

        [Fact]
        public async Task GetEmailById_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _emailService.GetEmailById(invalidId));
        }

        [Fact]
        public async Task GetEmailById_EmailNotFound_ThrowsEmailNotFoundException()
        {
            // Arrange
            var id = 1;
            _emailRepository.GetEmailById(id).Returns(Task.FromResult<Email>(null));

            // Act & Assert
            await Assert.ThrowsAsync<EmailNotFoundException>(() => _emailService.GetEmailById(id));
        }
    }
}

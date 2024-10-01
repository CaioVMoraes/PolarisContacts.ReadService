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
    public class TelefoneUnitTests
    {
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly TelefoneService _telefoneService;

        public TelefoneUnitTests()
        {
            _telefoneRepository = Substitute.For<ITelefoneRepository>();
            _telefoneService = new TelefoneService(_telefoneRepository);
        }

        // Teste para GetTelefonesByIdContato
        [Fact]
        public async Task GetTelefonesByIdContato_ValidId_ReturnsTelefones()
        {
            // Arrange
            var idContato = 1;
            var expectedTelefones = new List<Telefone>
            {
                new Telefone { Id = 1, NumeroTelefone = "1234-5678" },
                new Telefone { Id = 2, NumeroTelefone = "9876-5432" }
            };

            _telefoneRepository.GetTelefonesByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Telefone>>(expectedTelefones));

            // Act
            var result = await _telefoneService.GetTelefonesByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Telefone>)result).Count); // Verifica se foram retornados dois telefones
        }

        [Fact]
        public async Task GetTelefonesByIdContato_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _telefoneService.GetTelefonesByIdContato(invalidId));
        }

        // Teste para GetTelefoneById
        [Fact]
        public async Task GetTelefoneById_ValidId_ReturnsTelefone()
        {
            // Arrange
            var id = 1;
            var expectedTelefone = new Telefone { Id = id, NumeroTelefone = "1234-5678" };

            _telefoneRepository.GetTelefoneById(id).Returns(Task.FromResult(expectedTelefone));

            // Act
            var result = await _telefoneService.GetTelefoneById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTelefone.Id, result.Id);
        }

        [Fact]
        public async Task GetTelefoneById_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _telefoneService.GetTelefoneById(invalidId));
        }

        [Fact]
        public async Task GetTelefoneById_TelefoneNotFound_ThrowsTelefoneNotFoundException()
        {
            // Arrange
            var id = 1;
            _telefoneRepository.GetTelefoneById(id).Returns(Task.FromResult<Telefone>(null));

            // Act & Assert
            await Assert.ThrowsAsync<TelefoneNotFoundException>(() => _telefoneService.GetTelefoneById(id));
        }
    }
}

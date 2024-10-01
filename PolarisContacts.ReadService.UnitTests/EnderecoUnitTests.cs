using NSubstitute;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static PolarisContacts.ConsumerService.Domain.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.UnitTests
{
    public class EnderecoUnitTests
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly EnderecoService _enderecoService;

        public EnderecoUnitTests()
        {
            _enderecoRepository = Substitute.For<IEnderecoRepository>();
            _enderecoService = new EnderecoService(_enderecoRepository);
        }

        // Teste para GetEnderecosByIdContato
        [Fact]
        public async Task GetEnderecosByIdContato_ValidId_ReturnsEnderecos()
        {
            // Arrange
            var idContato = 1;
            var expectedEnderecos = new List<Endereco>
            {
                new Endereco { Id = 1, Logradouro = "Rua A", Cidade = "São Paulo", Estado = "SP" },
                new Endereco { Id = 2, Logradouro = "Rua B", Cidade = "Rio de Janeiro", Estado = "RJ" }
            };

            _enderecoRepository.GetEnderecosByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Endereco>>(expectedEnderecos));

            // Act
            var result = await _enderecoService.GetEnderecosByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Endereco>)result).Count); // Verifica se foram retornados dois endereços
        }

        [Fact]
        public async Task GetEnderecosByIdContato_InvalidId_ThrowsContatoNotFoundException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ContatoNotFoundException>(() => _enderecoService.GetEnderecosByIdContato(invalidId));
        }

        // Teste para GetEnderecoById
        [Fact]
        public async Task GetEnderecoById_ValidId_ReturnsEndereco()
        {
            // Arrange
            var id = 1;
            var expectedEndereco = new Endereco { Id = id, Logradouro = "Rua A", Cidade = "São Paulo", Estado = "SP" };

            _enderecoRepository.GetEnderecoById(id).Returns(Task.FromResult(expectedEndereco));

            // Act
            var result = await _enderecoService.GetEnderecoById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEndereco.Id, result.Id);
        }

        [Fact]
        public async Task GetEnderecoById_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _enderecoService.GetEnderecoById(invalidId));
        }

        [Fact]
        public async Task GetEnderecoById_EnderecoNotFound_ThrowsEnderecoNotFoundException()
        {
            // Arrange
            var id = 1;
            _enderecoRepository.GetEnderecoById(id).Returns(Task.FromResult<Endereco>(null));

            // Act & Assert
            await Assert.ThrowsAsync<EnderecoNotFoundException>(() => _enderecoService.GetEnderecoById(id));
        }
    }
}

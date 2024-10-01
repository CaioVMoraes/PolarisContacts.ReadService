using NSubstitute;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.ReadService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static PolarisContacts.ConsumerService.Domain.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.UnitTests
{
    public class CelularUnitTests
    {
        private readonly ICelularRepository _celularRepository;
        private readonly CelularService _celularService;

        public CelularUnitTests()
        {
            _celularRepository = Substitute.For<ICelularRepository>();
            _celularService = new CelularService(_celularRepository);
        }

        // Teste para GetCelularesByIdContato
        [Fact]
        public async Task GetCelularesByIdContato_ValidId_ReturnsCelulares()
        {
            // Arrange
            var idContato = 1;
            var expectedCelulares = new List<Celular>
            {
                new Celular { Id = 1, NumeroCelular = "91234-5678" },
                new Celular { Id = 2, NumeroCelular = "98765-4321" }
            };

            _celularRepository.GetCelularesByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Celular>>(expectedCelulares));

            // Act
            var result = await _celularService.GetCelularesByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Celular>)result).Count); // Confirma que dois celulares foram retornados
        }

        [Fact]
        public async Task GetCelularesByIdContato_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _celularService.GetCelularesByIdContato(invalidId));
        }

        // Teste para GetCelularById
        [Fact]
        public async Task GetCelularById_ValidId_ReturnsCelular()
        {
            // Arrange
            var id = 1;
            var expectedCelular = new Celular { Id = id, NumeroCelular = "91234-5678" };

            _celularRepository.GetCelularById(id).Returns(Task.FromResult(expectedCelular));

            // Act
            var result = await _celularService.GetCelularById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCelular.Id, result.Id);
        }

        [Fact]
        public async Task GetCelularById_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _celularService.GetCelularById(invalidId));
        }

        [Fact]
        public async Task GetCelularById_CelularNotFound_ThrowsCelularNotFoundException()
        {
            // Arrange
            var id = 1;
            _celularRepository.GetCelularById(id).Returns(Task.FromResult<Celular>(null));

            // Act & Assert
            await Assert.ThrowsAsync<CelularNotFoundException>(() => _celularService.GetCelularById(id));
        }
    }
}

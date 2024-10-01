using NSubstitute;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PolarisContacts.ReadService.UnitTests
{
    public class ContatoUnitTests
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly ICelularRepository _celularRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IRegiaoService _regiaoService;
        private readonly ContatoService _contatoService;

        public ContatoUnitTests()
        {
            _contatoRepository = Substitute.For<IContatoRepository>();
            _telefoneRepository = Substitute.For<ITelefoneRepository>();
            _celularRepository = Substitute.For<ICelularRepository>();
            _emailRepository = Substitute.For<IEmailRepository>();
            _enderecoRepository = Substitute.For<IEnderecoRepository>();
            _regiaoService = Substitute.For<IRegiaoService>();

            _contatoService = new ContatoService(
                _contatoRepository, _telefoneRepository, _celularRepository,
                _emailRepository, _enderecoRepository, _regiaoService);
        }

        // Teste para GetAllContatosByIdUsuario
        [Fact]
        public async Task GetAllContatosByIdUsuario_ValidId_ReturnsContatos()
        {
            // Arrange
            var idUsuario = 1;
            var contatos = new List<Contato>
            {
                new Contato { Id = 1, Nome = "Contato 1" },
                new Contato { Id = 2, Nome = "Contato 2" }
            };

            _contatoRepository.GetAllContatosByIdUsuario(idUsuario).Returns(contatos);
            _telefoneRepository.GetTelefonesByIdContato(Arg.Any<int>()).Returns(new List<Telefone>());
            _celularRepository.GetCelularesByIdContato(Arg.Any<int>()).Returns(new List<Celular>());
            _emailRepository.GetEmailsByIdContato(Arg.Any<int>()).Returns(new List<Email>());
            _enderecoRepository.GetEnderecosByIdContato(Arg.Any<int>()).Returns(new List<Endereco>());

            // Act
            var result = await _contatoService.GetAllContatosByIdUsuario(idUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllContatosByIdUsuario_NoContatos_ReturnsEmptyList()
        {
            // Arrange
            var idUsuario = 1;
            _contatoRepository.GetAllContatosByIdUsuario(idUsuario).Returns(new List<Contato>());

            // Act
            var result = await _contatoService.GetAllContatosByIdUsuario(idUsuario);

            // Assert
            Assert.Empty(result);
        }

        // Teste para GetContatoByIdAsync
        [Fact]
        public async Task GetContatoByIdAsync_ValidId_ReturnsContato()
        {
            // Arrange
            var idContato = 1;
            var contato = new Contato { Id = idContato, Nome = "Contato Teste" };
            var telefones = new List<Telefone> { new Telefone { Id = 1, NumeroTelefone = "1234-5678" } };
            var celulares = new List<Celular> { new Celular { Id = 1, NumeroCelular = "9876-5432" } };
            var emails = new List<Email> { new Email { Id = 1, EnderecoEmail = "teste@teste.com" } };
            var enderecos = new List<Endereco> { new Endereco { Id = 1, Logradouro = "Rua Teste" } };

            _contatoRepository.GetContatoById(idContato).Returns(contato);
            _telefoneRepository.GetTelefonesByIdContato(idContato).Returns(telefones);
            _celularRepository.GetCelularesByIdContato(idContato).Returns(celulares);
            _emailRepository.GetEmailsByIdContato(idContato).Returns(emails);
            _enderecoRepository.GetEnderecosByIdContato(idContato).Returns(enderecos);            

            // Act
            var result = await _contatoService.GetContatoById(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contato.Id, result.Id);
            Assert.NotEmpty(result.Telefones);
            Assert.NotEmpty(result.Celulares);
            Assert.NotEmpty(result.Emails);
            Assert.NotEmpty(result.Enderecos);
        }

        [Fact]
        public async Task GetContatoByIdAsync_ContatoNotFound_ReturnsNull()
        {
            // Arrange
            var idContato = 1;
            _contatoRepository.GetContatoById(idContato).Returns((Contato)null);

            // Act
            var result = await _contatoService.GetContatoById(idContato);

            // Assert
            Assert.Null(result);
        }

        // Teste para SearchContatosByIdUsuario
        [Fact]
        public async Task SearchContatosByIdUsuario_ValidTerm_ReturnsContatos()
        {
            // Arrange
            var idUsuario = 1;
            var searchTerm = "Contato";
            var contatos = new List<Contato>
            {
                new Contato { Id = 1, Nome = "Contato 1" }
            };

            _contatoRepository.SearchByUsuarioIdAndTerm(idUsuario, searchTerm).Returns(contatos);
            _telefoneRepository.GetTelefonesByIdContato(Arg.Any<int>()).Returns(new List<Telefone>());
            _celularRepository.GetCelularesByIdContato(Arg.Any<int>()).Returns(new List<Celular>());
            _emailRepository.GetEmailsByIdContato(Arg.Any<int>()).Returns(new List<Email>());
            _enderecoRepository.GetEnderecosByIdContato(Arg.Any<int>()).Returns(new List<Endereco>());

            // Act
            var result = await _contatoService.SearchContatosByIdUsuario(idUsuario, searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task SearchContatosByIdUsuario_NoResults_ReturnsEmptyList()
        {
            // Arrange
            var idUsuario = 1;
            var searchTerm = "NonExistent";
            _contatoRepository.SearchByUsuarioIdAndTerm(idUsuario, searchTerm).Returns(new List<Contato>());

            // Act
            var result = await _contatoService.SearchContatosByIdUsuario(idUsuario, searchTerm);

            // Assert
            Assert.Empty(result);
        }
    }
}

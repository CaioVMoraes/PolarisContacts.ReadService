using NSubstitute;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.ReadService.Domain;
using System.Threading.Tasks;
using Xunit;
using static PolarisContacts.ConsumerService.Domain.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.UnitTests
{
    public class UsuarioUnitTests
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioService _usuarioService;

        public UsuarioUnitTests()
        {
            _usuarioRepository = Substitute.For<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_usuarioRepository);
        }

        // Teste para verificar exceção de login vazio
        [Fact]
        public async Task GetUserByPasswordAsync_LoginVazio_ThrowsLoginVazioException()
        {
            // Arrange
            var login = string.Empty;
            var senha = "senha123";

            // Act & Assert
            await Assert.ThrowsAsync<LoginVazioException>(() => _usuarioService.GetUserByPasswordAsync(login, senha));
        }

        // Teste para verificar exceção de senha vazia
        [Fact]
        public async Task GetUserByPasswordAsync_SenhaVazia_ThrowsSenhaVaziaException()
        {
            // Arrange
            var login = "usuario123";
            var senha = string.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<SenhaVaziaException>(() => _usuarioService.GetUserByPasswordAsync(login, senha));
        }

        // Teste para verificar retorno de usuário válido
        [Fact]
        public async Task GetUserByPasswordAsync_ValidLoginAndSenha_ReturnsUsuario()
        {
            // Arrange
            var login = "usuario123";
            var senha = "senha123";
            var expectedUser = new Usuario
            {
                Id = 1,
                Login = login,
                Senha = senha,
                Ativo = true                
            };

            _usuarioRepository.GetUserByPasswordAsync(login, senha).Returns(expectedUser);

            // Act
            var result = await _usuarioService.GetUserByPasswordAsync(login, senha);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Login, result.Login);
            Assert.Equal(expectedUser.Senha, result.Senha);
            Assert.True(result.Ativo);
        }

        // Teste para verificar retorno null quando login ou senha são incorretos
        [Fact]
        public async Task GetUserByPasswordAsync_InvalidLoginOrSenha_ReturnsNull()
        {
            // Arrange
            var login = "usuario123";
            var senha = "senhaIncorreta";

            _usuarioRepository.GetUserByPasswordAsync(login, senha).Returns((Usuario)null);

            // Act
            var result = await _usuarioService.GetUserByPasswordAsync(login, senha);

            // Assert
            Assert.Null(result);
        }
    }
}

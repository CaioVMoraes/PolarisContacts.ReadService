using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;
using System.Threading.Tasks;
using static PolarisContacts.ConsumerService.Domain.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.Application.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<Usuario> GetUserByPasswordAsync(string login, string senha)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new LoginVazioException();
            }
            if (string.IsNullOrEmpty(senha))
            {
                throw new SenhaVaziaException();
            }

            return await _usuarioRepository.GetUserByPasswordAsync(login, senha);
        }
    }
}


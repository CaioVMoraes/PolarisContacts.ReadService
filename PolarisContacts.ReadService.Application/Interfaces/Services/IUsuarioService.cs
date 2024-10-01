using PolarisContacts.ReadService.Domain;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUserByPasswordAsync(string login, string senha);
    }
}
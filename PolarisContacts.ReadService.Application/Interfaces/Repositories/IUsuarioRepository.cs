using PolarisContacts.ReadService.Domain;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUserByPasswordAsync(string login, string senha);
    }
}
using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Services
{
    public interface IContatoService
    {
        Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario);
        Task<Contato> GetContatoById(int idContato);
        Task<IEnumerable<Contato>> SearchContatosByIdUsuario(int idUsuario, string searchTerm);
    }
}
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Repositories
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario);
        Task<Contato> GetContatoById(int idContato);
        Task<IEnumerable<Contato>> SearchByUsuarioIdAndTerm(int idUsuario, string searchTerm);
    }
}
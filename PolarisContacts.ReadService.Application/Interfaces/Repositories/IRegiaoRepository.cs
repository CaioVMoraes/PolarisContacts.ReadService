using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        Task<IEnumerable<Regiao>> GetAll();
        Task<Regiao> GetById(int idRegiao);
    }
}

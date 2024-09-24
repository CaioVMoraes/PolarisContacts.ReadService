using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato);
        Task<Endereco> GetEnderecoById(int id);
    }
}
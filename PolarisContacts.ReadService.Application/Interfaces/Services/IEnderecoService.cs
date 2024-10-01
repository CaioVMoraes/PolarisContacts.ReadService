using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato);
        Task<Endereco> GetEnderecoById(int id);
    }
}
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Services
{
    public class RegiaoService(IRegiaoRepository regiaoRepository) : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository = regiaoRepository;

        public async Task<IEnumerable<Regiao>> GetAll()
        {
            return await _regiaoRepository.GetAll();
        }

        public async Task<Regiao> GetById(int idRegiao)
        {
            return await _regiaoRepository.GetById(idRegiao);
        }
    }
}
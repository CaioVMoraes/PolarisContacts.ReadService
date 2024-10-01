using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.ConsumerService.Domain.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.Application.Services
{
    public class TelefoneService(ITelefoneRepository telefoneRepository) : ITelefoneService
    {
        private readonly ITelefoneRepository _telefoneRepository = telefoneRepository;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _telefoneRepository.GetTelefonesByIdContato(idContato);
        }

        public async Task<Telefone> GetTelefoneById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var telefone = await _telefoneRepository.GetTelefoneById(id);

            if (telefone is null)
            {
                throw new TelefoneNotFoundException();
            }

            return telefone;
        }

    }
}
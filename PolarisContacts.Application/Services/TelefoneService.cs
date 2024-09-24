using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
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
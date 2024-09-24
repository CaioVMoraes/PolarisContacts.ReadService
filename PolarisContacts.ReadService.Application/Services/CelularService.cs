using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.Application.Services
{
    public class CelularService(ICelularRepository celularRepository) : ICelularService
    {
        private readonly ICelularRepository _celularRepository = celularRepository;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _celularRepository.GetCelularesByIdContato(idContato);
        }

        public async Task<Celular> GetCelularById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var celular = await _celularRepository.GetCelularById(id);

            if (celular == null)
            {
                throw new CelularNotFoundException();
            }

            return celular;
        }
    }
}
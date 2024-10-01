using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.ConsumerService.Domain.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.Application.Services
{
    public class EnderecoService(IEnderecoRepository enderecoRepository) : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;

        public async Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new ContatoNotFoundException();
            }

            return await _enderecoRepository.GetEnderecosByIdContato(idContato);
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var endereco = await _enderecoRepository.GetEnderecoById(id);

            if (endereco == null)
            {
                throw new EnderecoNotFoundException();
            }

            return endereco;
        }
    }
}
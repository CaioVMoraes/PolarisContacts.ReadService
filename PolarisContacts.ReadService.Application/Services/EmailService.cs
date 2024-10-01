using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.ConsumerService.Domain.Exceptions.CustomExceptions;

namespace PolarisContacts.ReadService.Application.Services
{
    public class EmailService(IEmailRepository emailRepository) : IEmailService
    {
        private readonly IEmailRepository _emailRepository = emailRepository;

        public async Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _emailRepository.GetEmailsByIdContato(idContato);
        }

        public async Task<Email> GetEmailById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var email = await _emailRepository.GetEmailById(id);

            if (email == null)
            {
                throw new EmailNotFoundException();
            }

            return email;
        }
    }
}
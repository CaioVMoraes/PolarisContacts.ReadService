﻿using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Application.Interfaces.Services
{
    public interface ITelefoneService
    {
        Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato);
        Task<Telefone> GetTelefoneById(int id);
    }
}
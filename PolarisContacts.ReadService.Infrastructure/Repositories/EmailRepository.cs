﻿using Dapper;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.ReadService.Infrastructure.Repositories
{
    public class EmailRepository(IDatabaseConnection dbConnection) : IEmailRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Emails WHERE IdContato = @IdContato  AND Ativo = 1";
            return await conn.QueryAsync<Email>(query, new { IdContato = idContato });
        }

        public async Task<Email> GetEmailById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Emails WHERE Id = @Id  AND Ativo = 1";
            return await conn.QueryFirstOrDefaultAsync<Email>(query, new { Id = id });
        }
    }
}

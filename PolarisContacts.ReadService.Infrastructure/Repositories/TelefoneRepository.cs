using Dapper;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PolarisContacts.DatabaseConnection;

namespace PolarisContacts.ReadService.Infrastructure.Repositories
{
    public class TelefoneRepository(IDatabaseConnection dbConnection) : ITelefoneRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Telefones WHERE IdContato = @IdContato AND Ativo = 1";
            return await conn.QueryAsync<Telefone>(query, new { IdContato = idContato });
        }

        public async Task<Telefone> GetTelefoneById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Telefones WHERE Id = @Id  AND Ativo = 1";
            return await conn.QueryFirstOrDefaultAsync<Telefone>(query, new { Id = id });
        }

    }
}
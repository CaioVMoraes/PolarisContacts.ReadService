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
    public class CelularRepository(IDatabaseConnection dbConnection) : ICelularRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Celulares WHERE IdContato = @IdContato AND Ativo = 1";
            return await conn.QueryAsync<Celular>(query, new { IdContato = idContato });
        }

        public async Task<Celular> GetCelularById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Celulares WHERE Id = @Id AND Ativo = 1";
            return await conn.QueryFirstOrDefaultAsync<Celular>(query, new { Id = id });
        }
    }
}

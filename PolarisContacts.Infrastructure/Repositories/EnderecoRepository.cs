using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class EnderecoRepository(IDatabaseConnection dbConnection) : IEnderecoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Enderecos WHERE IdContato = @IdContato  AND Ativo = 1";

            return await conn.QueryAsync<Endereco>(query, new { IdContato = idContato });
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Enderecos WHERE Id = @Id  AND Ativo = 1";

            return await conn.QueryFirstOrDefaultAsync<Endereco>(query, new { Id = id });
        }

    }
}

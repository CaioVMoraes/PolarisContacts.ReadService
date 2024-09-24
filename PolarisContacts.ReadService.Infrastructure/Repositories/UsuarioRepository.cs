using Dapper;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Data;
using System.Threading.Tasks;
using PolarisContacts.DatabaseConnection;

namespace PolarisContacts.ReadService.Infrastructure.Repositories
{
    public class UsuarioRepository(IDatabaseConnection dbConnection) : IUsuarioRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<Usuario> GetUserByPasswordAsync(string login, string senha)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Usuarios WHERE [Login] = @Login AND Senha = @Senha AND Ativo = 1";

            return await conn.QueryFirstOrDefaultAsync<Usuario>(query, new { Login = login, Senha = senha });
        }
    }
}

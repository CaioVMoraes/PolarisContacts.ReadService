using Microsoft.Extensions.Options;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.Domain.Settings;
using System.Data;
using System.Data.SqlClient;

namespace PolarisContacts.ReadService.Infrastructure.Repositories
{
    public class DatabaseConnection(IOptions<DbSettings> dbSettings) : IDatabaseConnection
    {
        private readonly DbSettings _dbSettings = dbSettings.Value;

        public IDbConnection AbrirConexao()
        {
            var connection = new SqlConnection(_dbSettings.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}


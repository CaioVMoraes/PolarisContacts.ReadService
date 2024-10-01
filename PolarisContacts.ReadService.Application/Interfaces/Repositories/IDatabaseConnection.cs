using System.Data;

namespace PolarisContacts.ReadService.Application.Interfaces.Repositories
{
    public interface IDatabaseConnection
    {
        public IDbConnection AbrirConexao();
    }
}


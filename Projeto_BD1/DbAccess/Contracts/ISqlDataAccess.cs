using System.Data;

namespace Projeto_BD1.DbAccess.Contracts
{
    public interface ISqlDataAccess
    {
        public IDbConnection CreateConnection();
    }
}

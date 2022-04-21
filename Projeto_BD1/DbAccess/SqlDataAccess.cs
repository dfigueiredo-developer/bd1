using Projeto_BD1.DbAccess.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace Projeto_BD1.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
		private string _connectionString; //= @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=";
		private IConfiguration _config;

		public SqlDataAccess(IConfiguration config)
		{
			_config = config;
			_connectionString = _config.GetConnectionString("Default");
		}

		public IDbConnection CreateConnection()
			=> new SqlConnection(_connectionString);





	}
}

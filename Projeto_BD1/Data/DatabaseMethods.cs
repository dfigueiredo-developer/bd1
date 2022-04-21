using Dapper;
using Projeto_BD1.Data.Contracts;
using Projeto_BD1.DbAccess.Contracts;

namespace Projeto_BD1.Data
{
    public class DatabaseMethods : IDatabaseMethods
    {
        private ISqlDataAccess _context;

        public DatabaseMethods(ISqlDataAccess context)
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> crudArrayListAsync<T>(string query, DynamicParameters? parameters = null)
        {
            using var connection = _context.CreateConnection();

            var objectsArray = await connection.QueryAsync<T>(query, parameters);
            return objectsArray.ToList();
        }
    }
}

using Dapper;

namespace Projeto_BD1.Data.Contracts
{
    public interface IDatabaseMethods
    {
        public Task<IEnumerable<T>> crudArrayListAsync<T>(string query, DynamicParameters? parameters = null);


    }
}

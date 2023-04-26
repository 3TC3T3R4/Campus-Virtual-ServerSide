using System.Data;

namespace CampusVirtual.Infrastructure.SQLAdapter.Gateway
{
    public interface IDbConnectionBuilder
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}
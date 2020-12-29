using iToons.Library.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iToons.Repositories
{
    public interface IDataRepository
    {
        void MigrateDatabase();
        void AddMetaData(MetaData meta);
        void ClearMetaData();
        MetaData GetMetaData(int id);
        Task<List<User>> GetAllUsers();
    }
}

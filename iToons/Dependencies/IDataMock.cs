using iToons.Library.Entity;
using iToons.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iToons.Dependencies
{
    public class IDataMock: IDataRepository
    {
        public void MigrateDatabase()
        {
            throw new System.NotImplementedException();
        }

        public void AddMetaData(MetaData meta)
        {
            throw new System.NotImplementedException();
        }

        public void ClearMetaData()
        {
            throw new System.NotImplementedException();
        }

        public MetaData GetMetaData(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}

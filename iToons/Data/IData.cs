using iToons.Library.Entity;
using iToons.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iToons.Data
{
    public class IData : IDataRepository
    {
        private readonly IDataRepository DataRepository;

        public IData(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }

        public void MigrateDatabase()
        {
            DataRepository.MigrateDatabase();
        }

        public void AddMetaData(MetaData meta)
        {
            DataRepository.AddMetaData(meta);
        }

        public void ClearMetaData()
        {
            DataRepository.ClearMetaData();
        }

        public MetaData GetMetaData(int id)
        {
            return DataRepository.GetMetaData(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await DataRepository.GetAllUsers();
        }
    }
}

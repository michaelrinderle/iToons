using iToons.Library.Entity;
using System;

namespace iToons.Repositories
{
    public interface IMusicRepository
    {
        void GenerateMusicData();
        MetaData GetMetaData(int id);
        Byte[] GetSongStream(int id);
    }
}

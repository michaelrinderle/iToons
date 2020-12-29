using iToons.Library.Entity;
using iToons.Repositories;
using System;

namespace iToons.Data
{
    public class IMusic : IMusicRepository
    {
        private readonly IMusicRepository MusicRepository;

        public IMusic(IMusicRepository musicRepository)
        {
            MusicRepository = musicRepository;
        }

        public void GenerateMusicData()
        {
            MusicRepository.GenerateMusicData();
        }

        public MetaData GetMetaData(int id)
        {
            return MusicRepository.GetMetaData(id);
        }

        public Byte[] GetSongStream(int songId)
        {
            return MusicRepository.GetSongStream(songId);
        }
    }
}

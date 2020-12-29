using iToons.Library.Entity;
using iToons.Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace iToons.Dependencies
{
    public class IMusicFileDirectory : IMusicRepository
    {
        public async void GenerateMusicData()
        {
            Program.Data.ClearMetaData();
            var files = Directory.GetFiles(Constants.MusicDirectoryRoot);
            foreach (var f in files)
            {
                try
                {
                    var fileMetaData = TagLib.File.Create(f);
                    var meta = new MetaData();
                    meta.FileName = f;
                    // unlisting missing tags
                    meta.Title = fileMetaData.Tag?.Title != null ? fileMetaData.Tag?.Title : "Unlisted";
                    meta.Album = fileMetaData.Tag?.Album != null ? fileMetaData.Tag?.Album : "Unlisted";
                    meta.Artists = fileMetaData.Tag?.FirstPerformer != null ? fileMetaData.Tag?.FirstPerformer : "Unlisted";
                    meta.AlbumArtists = fileMetaData.Tag?.FirstAlbumArtist != null ? fileMetaData.Tag?.FirstAlbumArtist : "Unlisted";
                    meta.Genre = fileMetaData.Tag?.FirstGenre != null ? fileMetaData.Tag?.FirstGenre : "Unlisted";
                    // checking if mp3 has cover art otherwise replacing with default
                    if (!fileMetaData.Tag.Pictures.Any())
                    {
                        using var img = File.OpenRead(Constants.DefaultCoverArtPath);
                        var imageBytes = new byte[img.Length];
                        img.Read(imageBytes, 0, (int) img.Length);
                        meta.CoverArt = imageBytes;
                    }
                    else
                    {
                        meta.CoverArt = fileMetaData.Tag.Pictures[0].Data.Data;
                    }
                    Program.Data.AddMetaData(meta);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    Debug.WriteLine(f);
                    continue;
                }
            }
        }

        MetaData IMusicRepository.GetMetaData(int id)
        {
            return Program.Data.GetMetaData(id);
        }

        Byte[] IMusicRepository.GetSongStream(int id)
        {
            var meta = Program.Data.GetMetaData(id);
            var mp3Path = Path.Combine(Constants.MusicDirectoryRoot, meta.FileName);
            var buf = new byte[0];
            using (var fs = new FileStream(meta.FileName, FileMode.Open))
            {
                var br = new BinaryReader(fs);
                long numBytes = new FileInfo(mp3Path).Length;
                buf = br.ReadBytes((int) numBytes);
            }
            return buf;
        }
    }
}

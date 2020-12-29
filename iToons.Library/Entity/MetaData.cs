using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iToons.Library.Entity
{
    public class MetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artists { get; set; }
        public string AlbumArtists { get; set; }
        public string Genre { get; set; }
        public byte[] CoverArt { get; set; }
    }
}
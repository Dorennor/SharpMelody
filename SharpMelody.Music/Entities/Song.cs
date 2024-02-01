using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SharpMelody.Music.Entities
{
    //[Index(nameof(Title), IsUnique = true)]
    public class Song
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string[] Genres { get; set; }
        public uint Year { get; set; }
        public Guid PerformerId { get; set; }
        public Guid AlbumId { get; set; }
        [NotMapped]
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        [Column("Metadata")]
        private string MetadataJson
        {
            get
            {
                return JsonConvert.SerializeObject(Metadata, Formatting.Indented);
            }
        }
    }
}
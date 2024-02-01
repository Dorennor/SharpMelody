using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SharpMelody.Music.Entities
{
    //[Index(nameof(Title), IsUnique = true)]
    public class Album
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        [NotMapped]
        public List<Song> Songs { get; set; }
        public Guid PerformerId { get; set; }
        public string[] Genres { get; set; }
    }
}
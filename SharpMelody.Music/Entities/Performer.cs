using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SharpMelody.Music.Entities
{
    //[Index(nameof(Name), IsUnique = true)]
    public class Performer
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MetaGenre { get; set; }
        [NotMapped]
        public List<Album> Albums { get; set; }
    }
}
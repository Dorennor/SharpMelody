using Microsoft.EntityFrameworkCore;

using SharpMelody.Music.Entities;

namespace SharpMelody.Music.Data
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Performer> Performsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();

                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");

                var config = builder.Build();
                var connectionString = config.GetConnectionString("SQLiteConnection");

                optionsBuilder.UseSqlite(connectionString);
            }
        }

        public MusicDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FilmsWebApp.Models
{
    public class MovieContext : DbContext
    {
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenresOfFilm> GenresOfFilms { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}

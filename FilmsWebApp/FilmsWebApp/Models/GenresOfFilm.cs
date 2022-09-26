using System.ComponentModel.DataAnnotations;
namespace FilmsWebApp.Models
{
    public class GenresOfFilm
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int GenreId { get; set; }
        public virtual Film? Film { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
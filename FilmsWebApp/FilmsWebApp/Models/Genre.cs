using System.ComponentModel.DataAnnotations;
namespace FilmsWebApp.Models
{
    public class Genre
    {
        public Genre()
        {
            GenresOfFilm = new List<GenresOfFilm>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва жанру")]
        public string Name { get; set; }

        public virtual ICollection<GenresOfFilm>? GenresOfFilm { get; set; }

    }
}

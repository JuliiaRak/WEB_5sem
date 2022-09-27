using System.ComponentModel.DataAnnotations;
namespace FilmsWebApp.Models
{
    public class Film
    {
        public Film()
        {
            GenresOfFilm = new List<GenresOfFilm>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва фільму")]
        public string Name { get; set; }

        [Display(Name = "Кінокомпанія")]
        public int ProductionId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Тривалість")]
        public int Duration { get; set; }


        public virtual ICollection<GenresOfFilm>? GenresOfFilm { get; set; }
        public virtual Production? Production { get; set; }
    }
}

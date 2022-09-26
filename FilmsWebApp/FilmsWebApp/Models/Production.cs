using System.ComponentModel.DataAnnotations;
namespace FilmsWebApp.Models
{
    public class Production
    {
        public Production()
        {
            Films = new List<Film>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва кінокомпанії")]
        public string Name { get; set; }

        [Display(Name = "Продюсер")]
        public int DirectorId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Рік заснування")]
        public int YearOfFondation { get; set; }

        public virtual ICollection<Film>? Films { get; set; }
        public virtual Director? Director { get; set; }
    }
}

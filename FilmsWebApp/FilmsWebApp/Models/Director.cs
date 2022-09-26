using System.ComponentModel.DataAnnotations;
namespace FilmsWebApp.Models
{
    public class Director
    {
        public Director()
        {
            Productions = new List<Production>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ім'я продюсера")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Країна")]
        public string Country { get; set; }

        public virtual ICollection<Production>? Productions { get; set; }
    }
}


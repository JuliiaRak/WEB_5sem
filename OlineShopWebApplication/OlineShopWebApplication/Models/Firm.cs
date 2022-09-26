using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlineShopWebApplication
{
    public partial class Firm
    {
        public Firm()
        {
            Products = new HashSet<Product>();
        }

        public int FirmId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Фірма")]
        public string Name { get; set; } = null!;

        [Display(Name = "Рік створення")]
        [Range(1800, 2022, ErrorMessage = "Рік створення повинен бути між 1800-2022")]
        public int YearOfFondation { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Власник")]
        public string Owner { get; set; } = null!;

        [Display(Name = "Капітал")]
        [Range(0, double.MaxValue, ErrorMessage = "Капітал не може бути від'ємним")]
        public decimal? Capital { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}

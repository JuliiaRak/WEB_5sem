using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OlineShopWebApplication
{
    public partial class CustomerÀdress
    {

        [Display(Name = "Id адреси покупця")]
        public int CustomerAdressId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id покупця")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Адреса")]
        public string? Adress { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Широта")]
        public string? Long { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Довгота")]
        public string? Let { get; set; }

        [Display(Name = "Покупець")]
        public virtual Customer? Customer { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlineShopWebApplication
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerÀdresses = new HashSet<CustomerÀdress>();
            Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id покупця")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [EmailAddress(ErrorMessage = "Неправельне значення емайл")]
        [Display(Name = "Емайл")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ім'я")]
        public string? Name { get; set; }

        [Display(Name = "Рік народження")]
        [Range(1922, 2022, ErrorMessage = "Рік народження повинен бути між 1922-2022")]
        public int? YearOfBirth { get; set; }

        [Display(Name = "Номер телефону")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Непрвильний номер телефону, введіть 10 цифр")]
        public string? PhoneNumber { get; set; }

        public virtual ICollection<CustomerÀdress>? CustomerÀdresses { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}

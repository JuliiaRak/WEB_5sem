using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlineShopWebApplication
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            ProductImages = new HashSet<ProductImage>();
        }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id продукту")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ціна")]
        [Range(0, 150000, ErrorMessage = "Ціна може бути від 0 до 150000")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Рік")]
        [Range(1800, 2022, ErrorMessage = "Рік повинен бути між 1800-2022")]
        public int? Year { get; set; }
        //public byte[]? Image { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Силка на відео")]
        public string? NumberLeft { get; set; }

        //[Required(ErrorMessage = "Поле не повинне бути порожнім")]
        //[Display(Name = "Силка на")]
        //[Range(1, 3000, ErrorMessage = "Вага продукту може бути від 1 до 3000")]
        public string? ProductWeight { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Фірма")]
        public int FirmId { get; set; }

        [Display(Name = "Категорія")]
        public virtual Category? Category { get; set; } = null!;

        [Display(Name = "Фірма")]
        public virtual Firm? Firm { get; set; } = null!;
        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
    }
}

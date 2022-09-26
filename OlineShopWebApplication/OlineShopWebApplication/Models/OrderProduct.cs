using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlineShopWebApplication
{
    public partial class OrderProduct
    {
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id продукту з замовлення")]
        public int OrderProductId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id продукту")]
        public int ProductId { get; set; }
        //public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Кількість")]
        [Range(1, 100, ErrorMessage = "Кількість продуктів може бути від 1 до 100")]
        public int? NumberOfProducts { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id замовлення")]
        public int OrderId { get; set; }

        public virtual Order? Order { get; set; } = null!;
        public virtual Product? Product { get; set; } = null!;
    }
}

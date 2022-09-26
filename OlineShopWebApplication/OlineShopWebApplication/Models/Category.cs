using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlineShopWebApplication
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoriesId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Категорія")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Product>? Products { get; set; }
    }
}

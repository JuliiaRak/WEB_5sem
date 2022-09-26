using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlineShopWebApplication
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id замовлення")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id покупця")]
        public int CustomerId { get; set; }

        [Display(Name = "Дата замовлення")]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Id адреси покупця")]
        public int? CustomerAdressId { get; set; }

        public virtual Customer? Customer { get; set; } = null!;
        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}

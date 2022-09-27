using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlineShopWebApplication
{
    public partial class ProductImage
    {
        public int ProductImageId { get; set; }
        public byte[]? Image { get; set; }
        //public IFormFile Image { get; set; }
        public int ProductId { get; set; }
 
        public virtual Product? Product { get; set; } = null!;
    }
}

using Microsoft.AspNetCore.Identity;

namespace OlineShopWebApplication
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
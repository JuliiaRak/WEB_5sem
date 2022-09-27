using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace OlineShopWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly OnlineShopContext _context;

        public ChartController(OnlineShopContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var categories = _context.Categories.Include(c => c.Products).ToList();
            List<object> list = new List<object>();
            list.Add(new[] { "Категорії", "Кількість продуктів в категорії" });
            foreach (var c in categories)
            {
                list.Add(new object[] {c.Name, c.Products.Count()});
            }
            return new JsonResult(list);
        }

        [HttpGet("JsonData2")]
        public JsonResult JsonData2()
        {
            var firms = _context.Firms.Include(c => c.Products).ToList();
            List<object> list = new List<object>();
            list.Add(new[] { "Фірми", "Кількість продуктів даної фірми" });
            foreach (var c in firms)
            {
                list.Add(new object[] { c.Name, c.Products.Count() });
            }
            return new JsonResult(list);
        }


        [HttpGet("JsonDataCustomers")]
        public JsonResult JsonDataCustomers()
        {
            var customers = _context.Customers.Include(c => c.CustomerÀdresses).ToList();
            List<object> list = new List<object>();
            list.Add(new[] { "Покупці", "Кількість адрес покупця" });
            foreach (var c in customers)
            {
                list.Add(new object[] { c.Name, c.CustomerÀdresses.Count() });
            }
            return new JsonResult(list);
        }

        [HttpGet("JsonDataOrders")]
        public JsonResult JsonDataOrders()
        {
            var orders = _context.Orders.Include(c => c.OrderProducts).ToList();
            List<object> list = new List<object>();
            list.Add(new[] { "Замовлення", "Кількість продуктів в замовленні" });
            foreach (var c in orders)
            {
                list.Add(new object[] { c.OrderId, c.OrderProducts.Count() });
            }
            return new JsonResult(list);
        }
    }
}

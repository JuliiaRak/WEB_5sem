#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OlineShopWebApplication;
using Microsoft.AspNetCore.Authorization;

namespace OlineShopWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderProductsController : Controller
    {
        private readonly OnlineShopContext _context;

        public OrderProductsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: OrderProducts
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Orders", "Index");
            ViewBag.OrderId = id;
            var orderProductsByOrders = _context.OrderProducts.Where(o => o.OrderId == id).Include(o => o.Order).Include(o => o.Product);
                //OrderProducts.Include(o => o.Order).Include(o => o.Product);
            return View(await orderProductsByOrders.ToListAsync());
        }

        // GET: OrderProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderProductId == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // GET: OrderProducts/Create
        public IActionResult Create( int orderId)
        {
            //ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewBag.OrderId = orderId;
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int orderId, [Bind("OrderProductId,ProductId,Price,NumberOfProducts,OrderId")] OrderProduct orderProduct)
        {
            orderProduct.OrderId = orderId;
            
                if (ModelState.IsValid)
                {
                    _context.Add(orderProduct);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("Index", "OrderProducts", new { id = orderId });
                }

            //ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderProduct.OrderId);
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderProduct.ProductId);
            return RedirectToAction("Index", "OrderProducts", new { id = orderId });
            //return View(orderProduct);
        }

        // GET: OrderProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderProduct.ProductId);
            return View(orderProduct);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderProductId,ProductId,Price,NumberOfProducts,OrderId")] OrderProduct orderProduct)
        {
            if (id != orderProduct.OrderProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductExists(orderProduct.OrderProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "OrderProducts", new { id = id });
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderProduct.ProductId);
            return View(orderProduct);
        }

        // GET: OrderProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderProductId == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "OrderProducts", new { id = id });
        }

        private bool OrderProductExists(int id)
        {
            return _context.OrderProducts.Any(e => e.OrderProductId == id);
        }

    }
}

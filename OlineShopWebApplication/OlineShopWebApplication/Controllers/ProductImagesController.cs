#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OlineShopWebApplication;
using System.Web;
using System.IO;
//using System.Web.Mvc;

namespace OlineShopWebApplication.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly OnlineShopContext _context;

        public ProductImagesController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Product", "Index");
            ViewBag.ProductId = id;
            ViewBag.ProductName = name;
            //var onlineShopContext = _context.ProductImages.Include(p => p.Product);
            var ImagesByProduct = _context.ProductImages.Where(p => p.ProductId == id).Include(p => p.Product);
            return View(await ImagesByProduct.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public IActionResult Create(int productId)
        {
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewBag.ProductId = productId;
            ViewBag.ProductName = _context.Products.Where(p=> p.ProductId == productId).FirstOrDefault().Name;
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, [Bind("ProductImageId,Image,ProductId")] ProductImage productImage, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if(Image != null)
                {
                    productImage.Image = new byte[Image.Length];
                    Image.OpenReadStream (); //productImage.Image, 0, Image.Length
                }
                //_context.ProductImages.Add(Image);
                _context.Add(productImage);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "ProductImages", new { id = productId, name = _context.Products.Where(c => c.ProductId == productId).FirstOrDefault().Name });
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productImage.ProductId);
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productImage.ProductId);
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductImageId,Image,ProductId")] ProductImage productImage)
        {
            if (id != productImage.ProductImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.ProductImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productImage.ProductId);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.ProductImageId == id);
        }
    }
}

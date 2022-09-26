#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OlineShopWebApplication;

namespace OlineShopWebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OnlineShopContext _context;

        public ProductsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? property, int? id, string? name)
        {
            //var onlineShopContext = _context.Products.Include(p => p.Category).Include(p => p.Firm);
            //return View(await onlineShopContext.ToListAsync());
            if (property == "Category")
            {
                if (id == null) return RedirectToAction("Index", "Category");
                ViewBag.Id = id;
                ViewBag.Name = name;
                ViewBag.Property = property;
                //var onlineShopContext = ry
                var productsByCategory = _context.Products.Where(p => p.CategoryId == id).Include(p => p.Category).Include(p => p.Firm); //або тут було ще щось з фірмами
                return View(await productsByCategory.ToListAsync());
            }
            else if (property == "Firm")
            {
                if (id == null) return RedirectToAction("Index", "Firm");
                ViewBag.Id = id;
                ViewBag.Name = name;
                ViewBag.Property = property;
                var productsByFirm = _context.Products.Where(p => p.FirmId == id).Include(p => p.Firm).Include(p => p.Category);
                return View(await productsByFirm.ToListAsync());
            }
            //return RedirectToAction( "Index", "Category");
            return NotFound();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Firm)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
            //return RedirectToAction("Index", "ProductImages", new { id = product.ProductId, name = product.Name });
        }

        // GET: Products/Create
        public IActionResult Create(string Property, int Id)
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoriesId", "Name");
            //ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId");
            //return View();

            ViewBag.Id = Id;
            ViewBag.Property = Property;
            if (Property == "Category")
            {
                ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "Name");
                
                ViewBag.Name = _context.Categories.Where(p => p.CategoriesId == Id).FirstOrDefault().Name;
            }
            else if (Property == "Firm")
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoriesId", "Name");
                ViewBag.Name = _context.Firms.Where(p => p.FirmId == Id).FirstOrDefault().Name;              
            }
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Property, int Id, [Bind("ProductId,Name,CategoryId,Price,Year,NumberLeft,ProductWeight,FirmId")] Product product) // забрали Image
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoriesId", "Name", product.CategoryId);
            ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId", product.FirmId);
            return View(product);*/

            ViewBag.Property = Property;
            if (Property == "Category") { product.CategoryId = Id; }
            else if (Property == "Firm") { product.FirmId = Id; }
            if (!IsDuplicate(product))
            {
                //if (ModelState.IsValid)
                //{
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    if (Property == "Category")  // НЕ БАЧИТЬ
                    { return RedirectToAction("Index", "Products", new { property = Property, id = Id, name = _context.Categories.Where(p => p.CategoriesId == Id).FirstOrDefault().Name }); }
                    else if (Property == "Firm")
                    { return RedirectToAction("Index", "Products", new { property = Property, id = Id, name = _context.Firms.Where(p => p.FirmId == Id).FirstOrDefault().Name }); }
                    return NotFound();   //return RedirectToAction("Index", "Products", new {id = Id, name = _context.Categories.Where(p=> p.CategoriesId == Id).FirstOrDefault().Name});
                //}
            }
            else
            {
                ModelState.AddModelError("Name", "Такий продукт уже існує");
                if (Property == "Category") ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "Name", product.FirmId);
                else if (Property == "Firm") ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoriesId", "Name", product.CategoryId);
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoriesId", "Name", product.CategoryId);
            //ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId", product.FirmId);

            return View(product);

            /*if (Property == "Category")
            { return RedirectToAction("Index", "Products", new { property = Property, id = Id, name = _context.Categories.Where(p => p.CategoriesId == Id).FirstOrDefault().Name }); }
            else if (Property == "Firm")
            { return RedirectToAction("Index", "Products", new { property = Property, id = Id, name = _context.Firms.Where(p => p.FirmId == Id).FirstOrDefault().Name }); }
            return NotFound();*/
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string? Property, int? id) //
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoriesId", "Name", product.CategoryId);
            ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "Name", product.FirmId);
            ViewBag.Property = Property;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Property, int id, [Bind("ProductId,Name,CategoryId,Price,Year,NumberLeft,ProductWeight,FirmId")] Product product) // забрали Image
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                if (Property == "Category")
                { return RedirectToAction("Index", "Products", new { property = Property, id = id, name = _context.Categories.Where(p => p.CategoriesId == id).FirstOrDefault().Name }); }
                else if (Property == "Firm")
                { return RedirectToAction("Index", "Products", new { property = Property, id = id, name = _context.Firms.Where(p => p.FirmId == id).FirstOrDefault().Name }); }
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoriesId", "Name", product.CategoryId);
            ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId", product.FirmId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string? Property, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Firm)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Property, int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            if (Property == "Category")
            { return RedirectToAction("Index", "Products", new { property = Property, id = id, name = _context.Categories.Where(p => p.CategoriesId == id).FirstOrDefault().Name }); }
            else if (Property == "Firm")
            { return RedirectToAction("Index", "Products", new { property = Property, id = id, name = _context.Firms.Where(p => p.FirmId == id).FirstOrDefault().Name }); }
            return NotFound();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        private bool IsDuplicate(Product model)
        {
            var cat = _context.Products.FirstOrDefault(c => c.Name.Equals(model.Name));
            return cat == null ? false : true;
        }
    }
}

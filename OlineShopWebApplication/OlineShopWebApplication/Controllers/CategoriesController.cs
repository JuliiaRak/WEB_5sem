#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OlineShopWebApplication;
using ClosedXML.Excel;

namespace OlineShopWebApplication.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly OnlineShopContext _context;

        public CategoriesController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoriesId == id);
            if (category == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Products", new { property = "Category", id = category.CategoriesId, name = category.Name });

        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriesId,Name")] Category category)
        {
            if (!IsDuplicate(category))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                //return View(await _context.Categories.ToListAsync());
                return View(category);
            }
            else
                ModelState.AddModelError("Name", "Така категорія уже існує");
            return View(category);
            //return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriesId,Name")] Category category)
        {
            if (id != category.CategoriesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoriesId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoriesId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoriesId == id);
        }

        private bool IsDuplicate(Category model)
        {
            var cat = _context.Categories.FirstOrDefault(c => c.Name.Equals(model.Name));
            return cat == null ? false : true;
        }





        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            //RemoveUnmentionedProductions(workBook.Worksheets.Select(w => w.Name.ToLower()).ToList());
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                ProcessImportedCategories(worksheet);
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var categories = _context.Categories.Include("Products").ToList();

                foreach (var p in categories)
                {
                    var worksheet = workbook.Worksheets.Add(p.Name + "a");

                    worksheet.Cell("A1").Value = "Назва";
                    worksheet.Cell("B1").Value = "Ціна";
                    worksheet.Cell("C1").Value = "Рік";
                    worksheet.Cell("D1").Value = "Залишок";
                    worksheet.Cell("E1").Value = "Вага";
                    worksheet.Cell("F1").Value = "Id фірми";
                    //worksheet.Cell("G1").Value = "Номери замовленнь";
                    worksheet.Row(1).Style.Font.Bold = true;
                    var products = p.Products.ToList();

                    //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                    for (int i = 0; i < products.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = products[i].Name;
                        worksheet.Cell(i + 2, 2).Value = products[i].Price;
                        worksheet.Cell(i + 2, 3).Value = products[i].Year;
                        worksheet.Cell(i + 2, 4).Value = products[i].NumberLeft;
                        worksheet.Cell(i + 2, 5).Value = products[i].ProductWeight;
                        worksheet.Cell(i + 2, 6).Value = products[i].FirmId;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        //змініть назву файла відповідно до тематики Вашого проєкту

                        FileDownloadName = $"ProductBase_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        private void RemoveUnmentionedProductions(List<string> categoryNamesInLowerCase)
        {
            var categories = _context.Categories.Where(p =>
                !categoryNamesInLowerCase.Contains(p.Name.ToLower())).ToList();

            foreach (var category in categories)
            {
                _context.Categories.Remove(category);
            }
        }

        private void ProcessImportedCategories(IXLWorksheet worksheet)
        {
            //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову
            Category category = _context.Categories
                .FirstOrDefault(p => p.Name.ToLower() == worksheet.Name.ToLower());

            if (category == null)
            {
                category = new Category();
                category.Name = worksheet.Name + "EXCEL";
                //production.Country = "from Excel";

                //додати в контекст
                _context.Categories.Add(category);
            }

            //RemoveUnmentionedProducts(
              //  worksheet.RowsUsed().Skip(1).Select(r => r.Cell(1).GetString().ToLower()).ToList(),
              //  category.CategoriesId);

            //перегляд усіх рядків                    
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                ProcessImportedProduct(row, category);
            }
        }

        private void RemoveUnmentionedProducts(List<string> productsNamesInLowerCase, int categoryId)
        {
            var products = _context.Products.Where(m =>
                !productsNamesInLowerCase.Contains(m.Name.ToLower()) && m.CategoryId == categoryId).ToList();

            foreach (var product in products)
            {
                _context.Products.Remove(product);
            }
        }

        private void ProcessImportedProduct(IXLRow row, Category category)
        {
            /*Product product = _context.Products
                .Include(m => m.OrderProducts)
                .FirstOrDefault(m => m.Name.ToLower() == row.Cell(1).GetString().ToLower());

            if (product != null)
            {
                product.Price = row.Cell(2).GetValue<double>();
                product.Year = row.Cell(3).GetValue<int>();
                product.NumberLeft = row.Cell(4).GetValue<int>();
                product.ProductWeight = row.Cell(5).GetValue<double>();
                product.FirmId = row.Cell(6).GetValue<int>();
                product.OrderProducts = new List<OrderProduct>();
                _context.Products.Update(product);
            }

            else
            {}

            Product product = new Product();
            product.Name = row.Cell(1).Value.ToString();
            product.Price = row.Cell(2).GetValue<double>();
                product.Year = row.Cell(3).GetValue<int>();
                product.NumberLeft = row.Cell(4).GetValue<int>();
                product.ProductWeight = row.Cell(5).GetValue<double>();
                product.FirmId = row.Cell(6).GetValue<int>();

            product.Category = category;
                product.OrderProducts = new List<OrderProduct>();
                _context.Products.Add(product);
            

            //у разі наявності замовлення знайти його, у разі відсутності - додати
            int i = 7;
            while (!string.IsNullOrWhiteSpace(row.Cell(i).GetString()))
            {
                ProcessImportedOrder(row.Cell(i).GetDouble(), product);
                i++;
            }
        }

        private void ProcessImportedOrder(double orderId, Product product)
        {
            Order order = _context.Orders
                .FirstOrDefault(g => g.OrderId == (int)orderId); /////////////////////////////////////////////

            if (order == null)
            { }

            //Order order = new Order();
            //order = new Order();
                //order.OrderId = (int)orderId;
                //_context.Add(order);
            

            OrderProduct orderProduct = new OrderProduct();

            orderProduct.Product = product;
            orderProduct.Order = order;
            orderProduct.NumberOfProducts = 5;
            _context.OrderProducts.Add(orderProduct);
        }*/
    }
}
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
    public class CustomerÀdressController : Controller
    {
        private readonly OnlineShopContext _context;

        public CustomerÀdressController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: CustomerÀdress
        public async Task<IActionResult> Index( int? id, string? name)
        {
            //var onlineShopContext = _context.CustomerÀdresses.Include(c => c.Customer);
            //return View(await onlineShopContext.ToListAsync());
            if (id == null) return RedirectToAction("Customers", "Index");
            ViewBag.CustomerId = id;
            ViewBag.CustomerName = name;
            var CustomerAdressesByCustomer = _context.CustomerÀdresses.Where(c => c.CustomerId == id).Include(c => c.Customer);
            return View(await CustomerAdressesByCustomer.ToListAsync());
        }

        public async Task<IActionResult> IndexMap()
        {
            return View(await _context.CustomerÀdresses.ToListAsync());
        }

        // GET: CustomerÀdress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerÀdress = await _context.CustomerÀdresses
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerAdressId == id);
            if (customerÀdress == null)
            {
                return NotFound();
            }

            return View(customerÀdress);
        }

        // GET: CustomerÀdress/Create
        public IActionResult Create( int customerId)
        {
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerName = _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault().Name;
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email");
            return View();
        }

        // POST: CustomerÀdress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int customerId, [Bind("CustomerAdressId,CustomerId,Adress")] CustomerÀdress customerÀdress)
        {
            customerÀdress.CustomerId = customerId;
            if (!IsDuplicate(customerÀdress))
            {
                //if (ModelState.IsValid)
                //{
                _context.Add(customerÀdress);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "CustomerÀdress", new { id = customerId, name = _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault().Name });
                //}
            }
            else
            {
                ModelState.AddModelError("Name", "Така адреса уже існує");
                //customerÀdress.CustomerId = customerId;
            }
            return View(customerÀdress);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", customerÀdress.CustomerId);
            //return RedirectToAction("Index", "CustomerÀdress", new { id = customerId, name = _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault().Name });
        }

        // GET: CustomerÀdress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerÀdress = await _context.CustomerÀdresses.FindAsync(id);
            if (customerÀdress == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", customerÀdress.CustomerId);
            return View(customerÀdress);
        }

        // POST: CustomerÀdress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerAdressId,CustomerId,Adress")] CustomerÀdress customerÀdress)
        {
            if (id != customerÀdress.CustomerAdressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerÀdress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerÀdressExists(customerÀdress.CustomerAdressId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "CustomerÀdress", new { id = id, name = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault().Name });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", customerÀdress.CustomerId);
            return View(customerÀdress);
        }

        // GET: CustomerÀdress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerÀdress = await _context.CustomerÀdresses
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerAdressId == id);
            if (customerÀdress == null)
            {
                return NotFound();
            }

            return View(customerÀdress);
        }

        // POST: CustomerÀdress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerÀdress = await _context.CustomerÀdresses.FindAsync(id);
            _context.CustomerÀdresses.Remove(customerÀdress);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "CustomerÀdress", new { id = id, name = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault().Name });
        }

        private bool CustomerÀdressExists(int id)
        {
            return _context.CustomerÀdresses.Any(e => e.CustomerAdressId == id);
        }
        private bool IsDuplicate(CustomerÀdress model)
        {
            var cat = _context.CustomerÀdresses.FirstOrDefault(c => c.Adress.Equals(model.Adress));
            return cat == null ? false : true;
        }
    }
}

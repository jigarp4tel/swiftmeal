using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SwiftMeal.Data;
using SwiftMeal.Models;
using SwiftMeal.ViewModels;

namespace SwiftMeal.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SwiftMealContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(SwiftMealContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var productContext = _context.Product.Include(p => p.Category);
            return View(await productContext.ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var productContext = _context.Product.Include(p => p.Category);
            return View(await productContext.ToListAsync());
        }

        // GET: Products/Details/5
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,Description,Image,Price,CategoryID")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(productViewModel);

                Product product = new Product
                {
                    ProductName = productViewModel.ProductName,
                    Description = productViewModel.Description,
                    Image = uniqueFileName,
                    Price = productViewModel.Price,
                    CategoryID = productViewModel.CategoryID,
                    RestaurantID = productViewModel.RestaurantID,

                };

                _context.Add(product);
                ViewData["Category"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName", product.CategoryID);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));

            }

            return View();
        }

        private string UploadedFile(ProductViewModel product)
        {
            string uniqueFileName = null;

            if (product.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.Image.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

        // GET: Products/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,Description,Image,Price,CategoryID")] Product product)
        {
            if (id != product.ProductID)
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
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Products/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);

            //Delete image from root folder
            var image = Path.Combine(_webHostEnvironment.WebRootPath, "img", product.Image);
            if (System.IO.File.Exists(image))
            {
                System.IO.File.Delete(image);
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }
    }
}

using E_handelsapplikation.Data;
using E_handelsapplikation.Models;
using E_handelsapplikation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_handelsapplikation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public ProductsController(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            // Hämta produkter från databasen
            var localProducts = await _context.Products.ToListAsync();

            // Hämta produkter från Web API
            var apiProducts = await GetProductsFromApi();

            // Skapa en ViewModel som innehåller lokala och externa produkter
            var viewModel = new ProductViewModel
            {
                LocalProducts = localProducts,
                ApiProducts = apiProducts
            };

            return View(viewModel); // Skicka ViewModel till vyn
        }

        // Metod för att hämta produkter från Web API
        private async Task<List<Product>> GetProductsFromApi()
        {
            string apiURL = "http://localhost:5179/api/products";  // URL till  API

            var response = await _httpClient.GetAsync(apiURL);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
                return products;
            }
            else
            {
                return new List<Product>(); // Returnera en tom lista om API-anropet misslyckas
            }
        }

        // GET: Products/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit
        public async Task<IActionResult> Edit(int? id)
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
            return View(product);
        }

        // POST: Products/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Category")] Product product)
        {
            if (id != product.Id)
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
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Metod för att kontrollera om en produkt finns
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // GET: Products/Reviews
        public async Task<IActionResult> Reviews()
        {
            // Hämta recensioner från Web API
            var reviews = await GetReviewsFromApi();

            return View(reviews); // Skicka recensionerna till vyn
        }

        // Metod för att hämta recensioner från Web API
        private async Task<List<Review>> GetReviewsFromApi()
        {
            string apiURL = "http://localhost:5179/api/reviews";

            var response = await _httpClient.GetAsync(apiURL);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonString);
                return reviews;
            }
            else
            {
                return new List<Review>(); // Returnera en tom lista om API-anropet misslyckas
            }
        }
    }
}


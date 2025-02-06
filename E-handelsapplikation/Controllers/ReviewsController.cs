using Microsoft.AspNetCore.Mvc;
using E_handelsapplikation.Models;
using Newtonsoft.Json;
using System.Text;

namespace E_handelsapplikation.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            // Skicka en GET-förfrågan till WebAPI:t för att hämta recensioner
            var response = await _httpClient.GetAsync("http://localhost:5179/api/reviews");

            if (response.IsSuccessStatusCode)
            {
                // Läs in JSON-svaret och konvertera till en lista med recensioner
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonResponse);

                return View(reviews);
            }

            return View(new List<Review>());
        }

        // GET: Reviews/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Hämta specifik recension från WebAPI
            var response = await _httpClient.GetAsync($"http://localhost:5179/api/reviews/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var review = JsonConvert.DeserializeObject<Review>(jsonResponse);
                return View(review);
            }

            return NotFound();
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create";
            return View(new Review()); // Skickar en tom instans till vyn
        }


        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ReviewerName,Comment,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5179/api/reviews", review);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Ett fel inträffade vid skapandet av recensionen.");
            }
            return View(review);
        }

        // GET: Reviews/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"http://localhost:5179/api/reviews/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var review = JsonConvert.DeserializeObject<Review>(jsonResponse);
                return View(review);
            }

            return NotFound();
        }

        // POST: Reviews/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ReviewerName,Comment,Rating")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(review);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"http://localhost:5179/api/reviews/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                // Hantera fel om API-anropet misslyckas
                ModelState.AddModelError("", "Failed to update the review.");
            }

            return View(review);
        }

        // GET: Reviews/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"http://localhost:5179/api/reviews/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var review = JsonConvert.DeserializeObject<Review>(jsonResponse);
                return View(review);
            }

            return NotFound();
        }

        // POST: Reviews/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5179/api/reviews/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to delete the review.");
            return View(); // Returnerar vyn med eventuella felmeddelanden
        }

    }
}

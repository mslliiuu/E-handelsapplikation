using E_handelsapplikation.Models;

//Kod för att hämta produkter från lokal databas och externt API.
namespace E_handelsapplikation.ViewModels
{
    public class ProductViewModel
    {
        // Lista för produkter från databasen (lokala produkter)
        public List<Product> LocalProducts { get; set; }

        // Lista för produkter från API:et (externt API)
        public List<Product> ApiProducts { get; set; }
    }
}

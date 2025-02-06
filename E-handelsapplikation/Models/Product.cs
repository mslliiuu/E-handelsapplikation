namespace E_handelsapplikation.Models

{ //Klass som definerar egenskaper för produkt, inkluderar id, namn, beskrivning, pris och kategori.
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }    

    }
}

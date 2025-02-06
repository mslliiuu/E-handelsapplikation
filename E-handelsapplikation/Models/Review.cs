namespace E_handelsapplikation.Models
{
    //Klass för att definera egenskaper för recension, inkluderar id, produktnamn, namn på recensent, kommentar och betyg.
    public class Review
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ReviewerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}

namespace TBGestionStock.ASP.Models
{
    public class ProduitIndexVM
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Categories { get; set; }
    }
}

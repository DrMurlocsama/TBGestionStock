using TBGestionStock.DAL.Entitie;

namespace TBGestionStock.ASP.Models
{
    public class ProduitModifyVM
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ModificationStock { get; set; }
        public int? CategorieId { get; set; }

        public IEnumerable<Categorie>? categories { get; set; }
    }
}

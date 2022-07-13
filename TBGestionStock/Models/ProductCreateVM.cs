using TBGestionStock.DAL.Entitie;

namespace TBGestionStock.ASP.Models
{
    public class ProductCreateVM
    {
        public string? Name { get; set; }
        public string? Dscription { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategorieId { get; set; }

        public IEnumerable<Categorie>? categories { get; set; }

    }
}

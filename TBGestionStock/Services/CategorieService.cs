using TBGestionStock.DAL;
using TBGestionStock.DAL.Entitie;

namespace TBGestionStock.ASP.Services
{
    public class CategorieService
    {
        private readonly StockContext _dc;

        public CategorieService(StockContext dc)
        {
            _dc = dc;
        }
        public IEnumerable<Categorie> AppliqueCategorie()
        {
            return _dc.Categorias;
        }
    }
}

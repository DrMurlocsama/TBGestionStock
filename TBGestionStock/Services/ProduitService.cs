using TBGestionStock.ASP.Exeption;
using TBGestionStock.DAL;
using TBGestionStock.DAL.Entitie;

namespace TBGestionStock.ASP.Services
{
    public class ProduitService
    {
        private readonly StockContext _dc;
        private readonly MailService _mailService;
        private readonly CategorieService _categorieService;

        public ProduitService(StockContext dc, MailService mailService, CategorieService categorieService)
        {
            _dc = dc;
            _mailService = mailService;
            _categorieService = categorieService;
        }

        public List<Produit> GetProduits()
        {
            return _dc.Produits.Where(p => p.Deleted == false).OrderBy(p => p.Name).ToList();
        }
        public void CreateProduit(Produit p)
        {
            string firstLetters = p.Name.Replace(" ", "").Substring(0, 4);
            int nb = _dc.Produits.Where(t => t.Reference.StartsWith(firstLetters)).Count();
            p.Reference = firstLetters.ToUpper() + nb.ToString().PadLeft(4, '0');
            p.Deleted = false;
            p.CreateDate = DateTime.Now;
            p.UpdateDate = DateTime.Now;
            _dc.Produits.Add(p);
            _dc.SaveChanges();
        }

        public void DeleteProduit(Produit p)
        {
            decimal total = p.Stock * p.Prix;
            if (total >= 1000)
            {
                throw new ProduitExeptions("Impossible de supprimer l'exeption");
            }
            else if (total >= 100)
            {
                //envoyer mail
                _mailService.EnvoyerMail("Limite depense avertissement", $"le produit {p.Name} a été supprimé vous avez jeter {total}", "benjaminstephanedubray@gmail.com");
            }
            p.Deleted = true;
            p.UpdateDate= DateTime.Now;
            _dc.SaveChanges();
        }
        public Produit GetById(int id)
        {
            return _dc.Produits.Find(id);
        }
        public void Update(Produit p, int stockUpdate)
        {
            Produit produit = GetById(p.Id);
            produit.Prix = p.Prix;
            produit.Name = p.Name;
            produit.CategorieId = p.CategorieId;
            if (stockUpdate * p.Prix <= -1000)
            {
                _mailService.EnvoyerMail("Limite depense avertissement", $"le produit {p.Name} Vous etes infèrieur a 1000 euro", "benjaminstephanedubray@gmail.com");
            }
            else if (produit.Stock + stockUpdate < 0)
            {
                _mailService.EnvoyerMail("Vous est à avec un stock de 0",$"le produit {p.Name} à un stock de 0 ATTENTION", "benjaminstephanedubray@gmail.com");
            }
            if (produit.Stock + stockUpdate <= 0)
            {
                produit.Stock = 0;
            }
            else
            {
                produit.Stock += stockUpdate;
            }
            p.UpdateDate = DateTime.Now;
            _dc.SaveChanges();
        }
    }
}

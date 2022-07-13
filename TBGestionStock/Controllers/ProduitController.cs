using Microsoft.AspNetCore.Mvc;
using TBGestionStock.ASP.Models;
using TBGestionStock.ASP.Services;
using TBGestionStock.DAL.Entitie;

namespace TBGestionStock.ASP.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ProduitService _produitService;
        private readonly CategorieService _categorieService;

        public ProduitController(ProduitService produitService, CategorieService categorieService)
        {
            _produitService = produitService;
            _categorieService = categorieService;
        }

        public IActionResult Index()
        {
            List<Produit> products = _produitService.GetProduits();
            List<ProduitIndexVM> model = products.Select(p => new ProduitIndexVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Prix,
                Reference = p.Reference,

            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ProductCreateVM model = new ProductCreateVM();
            model.categories = _categorieService.AppliqueCategorie();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateVM model)
        {
            if (ModelState.IsValid)
            {
                Produit product = new Produit { Name = model.Name, Prix = model.Price / 100, Stock = model.Stock, Description = model.Dscription };
                try
                {
                    _produitService.CreateProduit(product);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw new Exception("Impossible d'entrer se produit");
                }
            };
            return View(model);
        }
        public IActionResult Confirmation([FromRoute] int id)
        {
            Produit? produit = _produitService.GetById(id);
            if (produit == null)
            {
                return NotFound();
            }

            ProduitDeleteVM model = new ProduitDeleteVM
            {
                Id = produit.Id,
                Name = produit.Name,
            };

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Produit p = _produitService.GetById(id);

            try
            {
                _produitService.DeleteProduit(p);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Modification([FromRoute] int id)
        {
            Produit? p = _produitService.GetById(id);
            if (p is null)
            {
                return NotFound();
            }
            ProduitModifyVM model = new ProduitModifyVM()
            {
                Id = p.Id,
                Reference = p.Reference,
                Name = p.Name,
                Price = p.Prix,
                Stock = p.Stock,
                CategorieId = p.CategorieId
            };
            model.categories = _categorieService.AppliqueCategorie();
            return View(model);
        }
        [HttpPost]
        public IActionResult Modification(ProduitModifyVM model)
        {
            if (ModelState.IsValid)
            {
                int StockUpdate = model.ModificationStock;

                Produit p = new Produit
                {
                    Id = model.Id,
                    Name = model.Name,
                    Prix = model.Price / 100,
                    Reference = model.Reference,
                    CategorieId = model.CategorieId
                };

                TempData["success"] = "Nouvelle catégorie ajouté!";
                _produitService.Update(p,StockUpdate);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

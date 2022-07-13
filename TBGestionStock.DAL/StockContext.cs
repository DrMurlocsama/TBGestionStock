using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGestionStock.DAL.Entitie;

namespace TBGestionStock.DAL
{
    public class StockContext : DbContext
    {
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Categorie> Categorias { get; set; }
        public StockContext(DbContextOptions options) : base(options)
        {

        }
    }
}

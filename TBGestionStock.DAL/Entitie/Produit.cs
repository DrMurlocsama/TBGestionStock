using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBGestionStock.DAL.Entitie
{
    public class Produit
    {
        public int Id { get; set; }


        [MaxLength(50), MinLength(4)]
        public string? Name { get; set; }


        [MaxLength(1000)]
        public string? Description { get; set; }


        [Column(TypeName = "char"), StringLength(8)]
        public string? Reference { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Prix { get; set; }


        public int Stock { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Categorie? Categorie { get; set; }

        public int? CategorieId { get; set; }
    }
}

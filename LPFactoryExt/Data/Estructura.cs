using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Estructuras")]
    public class Estructura
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Display(Name = "Artículo")]
        public int ArticuloId { get; set; }
        [Display(Name = "Artículo")]
        public Articulo Articulo { get; set; }

        [Display(Name = "Versión")]
        [StringLength(50)]
        public string Version { get; set; }

        public int Cantidad { get; set; }

        public bool Predeterminada { get; set; }

        public bool Activo { get; set; }

        public ICollection<EstructuraFase> Fases { get; set; }

        public string Descripcion
        {
            get
            {
                return $"{Articulo.CodigoYDescripcion} ({Version})";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("ArticuloFamilias")]
    public class ArticuloFamilia
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public int Actividad { get; set; }

        public bool Activo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string CodigoYDescripcion
        {
            get
            {
                return $"{Codigo} {Descripcion}";
            }
        }
    }
}

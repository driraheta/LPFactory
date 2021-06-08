using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Plantas")]
    public class Planta
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public string Direccion { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string CodigoYNombre
        {
            get
            {
                return $"{Codigo} {Nombre}";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Operaciones")]
    public class Operacion
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
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

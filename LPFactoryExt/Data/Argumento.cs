using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Argumentos")]
    public class Argumento
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public ICollection<ArgumentoValor> Valores { get; set; }

    }
}

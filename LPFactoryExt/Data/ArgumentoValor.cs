using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("ArgumentoValores")]
    public class ArgumentoValor
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public int ArgumentoId { get; set; }
        public Argumento Argumento { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }        

    }
}

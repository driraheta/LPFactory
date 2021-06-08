using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("IncidenciaTipos")]
    public class IncidenciaTipo
    {
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

    }
}

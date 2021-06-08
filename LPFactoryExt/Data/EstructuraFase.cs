using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("EstructurasFases")]
    public class EstructuraFase
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public int EstructuraId { get; set; }
        public Estructura Estructura { get; set; }

        [Display(Name = "Fase")]
        public int FaseId { get; set; }
        [Display(Name = "Fase")]
        public Fase Fase { get; set; }



        public ICollection<EstructuraFaseMaterial> Materiales { get; set; }
        
        public ICollection<EstructuraFaseOperacion> Operaciones { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("EstructurasFasesOperaciones")]
    public class EstructuraFaseOperacion
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public int EstructuraFaseId { get; set; }
        public EstructuraFase EstructuraFase { get; set; }

        [Display(Name = "Operación")]
        public int OperacionId { get; set; }
        [Display(Name = "Operación")]
        public Operacion Operacion { get; set; }

        [Display(Name = "Máquina")]
        public int MaquinaId { get; set; }
        [Display(Name = "Máquina")]
        public Maquina Maquina { get; set; }

        [Display(Name = "T. Prep.")]
        public int TiempoPreparacion { get; set; }

        [Display(Name = "T. Prod.")]
        public int TiempoProduccion { get; set; }

        [Display(Name = "Mermas Teo.")]
        public int MermasTeoricas { get; set; }

    }
}

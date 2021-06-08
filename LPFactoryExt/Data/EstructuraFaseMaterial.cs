using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    public class EstructuraFaseMaterial
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public int EstructuraFaseId { get; set; }
        public EstructuraFase EstructuraFase { get; set; }

        [Display(Name = "Artículo")]
        public int ArticuloId  { get; set; }
        [Display(Name = "Artículo")]
        public Articulo Articulo { get; set; }

        public int Cantidad { get; set; }

    }
}

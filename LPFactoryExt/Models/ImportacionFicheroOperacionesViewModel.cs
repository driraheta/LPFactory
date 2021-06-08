using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Models
{
    public class ImportacionFicheroOperacionesViewModel
    {
        [Display(Name = "Alta artículos")]
        public bool AltaArticulos { get; set; }

        [Display(Name = "Actualizar artículos existentes")]
        public bool ActualizarArticulosExistentes { get; set; }

        [Display(Name = "Generar órdenes")]
        public bool GenerarOrdenes { get; set; }

        [Display(Name = "Lanzar órdenes generadas")]
        public bool LanzarOrdenesGeneradas { get; set; }

        [Display(Name = "Duplicar órdenes existentes")]
        public bool DuplicarOrdenesExistentes { get; set; }

    }
}

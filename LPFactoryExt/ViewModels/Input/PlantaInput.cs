using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.ViewModels.Input
{
    public class PlantaInput
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public bool Activo { get; set; }
        
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

    }
}

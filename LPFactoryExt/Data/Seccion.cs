using Omu.AwesomeMvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Secciones")]
    public class Seccion
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public int Actividad { get; set; }

        public bool Activo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllPlantas", Controller = "Data")]
        [DisplayName("Planta")]
        public int PlantaId { get; set; }
        public Planta Planta { get; set; }



        public string CodigoYDescripcion
        {
            get
            {
                return $"{Codigo} {Descripcion}";
            }
        }
    }
}

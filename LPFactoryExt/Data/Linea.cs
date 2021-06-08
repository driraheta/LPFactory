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
    [Table("Lineas")]
    public class Linea
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllPlantas", Controller = "Data")]
        [DisplayName("Planta")]
        public int? PlantaId { get; set; }
        public Planta Planta { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllSecciones", Controller = "Data")]
        [DisplayName("Sección")]
        public int? SeccionId { get; set; }
        [DisplayName("Sección")]
        public Seccion Seccion { get; set; }

        public string CodigoYNombre
        {
            get
            {
                return $"{Codigo} {Nombre}";
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Omu.AwesomeMvc;

namespace LPFactory.Data
{
    [Table("Puestos")]
    public class Puesto
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllSecciones", Controller = "Data")]
        [DisplayName("Sección")]
        public int? SeccionId { get; set; }
        public Seccion Seccion { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllLineas", Controller = "Data")]
        [DisplayName("Línea")]
        public int? LineaId { get; set; }
        public Linea Linea { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllMaquinas", Controller = "Data")]
        [DisplayName("Maquina")]
        public int? MaquinaId { get; set; }
        public Maquina Maquina { get; set; }

    }
}

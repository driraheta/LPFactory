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
    [Table("Maquinas")]
    public class Maquina
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public string Modelo { get; set; }

        [DisplayName("Número serie")]
        public string NumeroSerie { get; set; }

        [DisplayName("Año fabricación")]
        public int AnioFabricacion { get; set; }

        [DisplayName("Versión")]
        public string Version { get; set; }

        public int Actividad { get; set; }

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
        [DisplayName("Línea")]
        public Linea Linea { get; set; }



        public string CodigoYDescripcion
        {
            get
            {
                return $"{Codigo} {Descripcion}";
            }
        }

    }
}

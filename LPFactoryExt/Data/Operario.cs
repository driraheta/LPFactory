using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Omu.AwesomeMvc;

namespace LPFactory.Data
{
    [Table("Operarios")]
    public class Operario
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public bool Entrada { get; set; }

        [Display(Name = "Ot. Abierta")]
        public bool OtAbierta { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllSecciones", Controller = "Data")]
        [DisplayName("Sección")]
        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }

        [Display(Name = "Hora Entrada 1")]
        public int HoraEntrada1 { get; set; }

        [Display(Name = "Minuto Entrada 1")]
        public int MinutoEntrada1 { get; set; }

        [Display(Name = "Hora Salida 1")]
        public int HoraSalida1 { get; set; }

        [Display(Name = "Minuto Salida 1")]
        public int MinutoSalida1 { get; set; }

        [Display(Name = "Hora Entrada 2")]
        public int HoraEntrada2 { get; set; }

        [Display(Name = "Minuto Entrada 2")]
        public int MinutoEntrada2 { get; set; }

        [Display(Name = "Hora Salida 2")]
        public int HoraSalida2 { get; set; }

        [Display(Name = "Minuto Salida 2")]
        public int MinutoSalida2 { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }


        public string CodigoYDescripcion
        {
            get
            {
                return $"{Codigo} {Nombre}";
            }
        }

    }
}

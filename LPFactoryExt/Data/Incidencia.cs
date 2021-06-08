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
    [Table("Incidencias")]
    public class Incidencia
    {
        public int Id { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllOrdenes", Controller = "Data")]
        [DisplayName("Orden")]
        public int OrdenId { get; set; }
        public Orden Orden { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllSecciones", Controller = "Data")]
        [DisplayName("Sección")]
        public int SeccionId { get; set; }
        [Display(Name = "Sección")]
        public Seccion Seccion { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllFases", Controller = "Data")]
        [DisplayName("Fase")]
        public int FaseId { get; set; }
        public Fase Fase { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllOperaciones", Controller = "Data")]
        [Display(Name = "Operación")]
        public int OperacionId { get; set; }
        [Display(Name = "Operación")]
        public Operacion Operacion { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllMaquinas", Controller = "Data")]
        [DisplayName("Maquina")]
        public int MaquinaId { get; set; }
        [Display(Name = "Máquina")]
        public Maquina Maquina { get; set; }

        public int Secuencia { get; set; }

        [Display(Name = "Nº Línea")]
        public int NumeroLinea { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArticulos", Controller = "Data")]
        [Display(Name = "Artículo")]
        public int ArticuloId { get; set; }
        [Display(Name = "Artículo")]
        public Articulo Articulo { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllIncidenciaTipo", Controller = "Data")]
        [Display(Name = "Tipo")]
        public int IncidenciaTipoId { get; set; }
        [Display(Name = "Tipo")]
        public IncidenciaTipo IncidenciaTipo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public int Tiempo { get; set; }

        public string Observaciones { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? Fecha { get; set; }

    }
}

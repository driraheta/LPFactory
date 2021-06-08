using LPFactory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Omu.AwesomeMvc;

namespace LPFactory.Data
{
    [Table("OrdenOperaciones")]
    public class OrdenOperacion
    {
        public int Id { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllOrdenes", Controller = "Data")]
        [Display(Name = "Orden")]
        public int OrdenId { get; set; }
        public Orden Orden { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllSecciones", Controller = "Data")]
        [Display(Name = "Sección")]
        public int SeccionId { get; set; }
        [Display(Name = "Sección")]
        public Seccion Seccion { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllFases", Controller = "Data")]
        [Display(Name = "Fase")]
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
        [Display(Name = "Máquina")]
        public int MaquinaId { get; set; }
        [Display(Name = "Máquina")]
        public Maquina Maquina { get; set; }

        public int Secuencia { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArticulos", Controller = "Data")]
        [Display(Name = "Artículo")]
        public int ArticuloId { get; set; }
        [Display(Name = "Artículo")]
        public Articulo Articulo { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "T. Prep.")]
        public int TiempoPreparacion { get; set; }

        [Display(Name = "T. Prod.")]
        public int TiempoProduccion { get; set; }

        public int Cantidad { get; set; }

        [Display(Name = "Mermas Teo.")]
        public int MermasTeoricas { get; set; }


        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllOperarios", Controller = "Data")]
        [Display(Name = "Operario")]
        public int? OperarioId { get; set; }
        [Display(Name = "Operario")]
        public Operario Operario { get; set; }

        [Display(Name = "T. Prep. Real")]
        public int TiempoPreparacionReal { get; set; }

        [Display(Name = "T. Prod. Real")]
        public int TiempoProduccionReal { get; set; }

        [Display(Name = "Cant. Producida")]
        public int CantidadProducida { get; set; }

        [Display(Name = "Mermas Real")]
        public int MermasReales { get; set; }

        [Display(Name = "T. Inci.")]
        public int TiempoIncidencias { get; set; }

        [Display(Name = "Nº Inci.")]
        public int NumeroIncidencias { get; set; }

        [Display(Name = "T. Microparadas")]
        public int TiempoMicroparadas { get; set; }

        [Display(Name = "Nº Microparadas")]
        public int NumeroMicroparadas { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha Fin")]
        public DateTime? FechaFin { get; set; }

        public OrdenEstado Estado { get; set; }

        public string Observaciones { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string CodigoOrden { get; set; }

        public string CodigoArticulo { get; set; }


    }
}

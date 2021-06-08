using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Omu.AwesomeMvc;

namespace LPFactory.Data
{
    [Table("OrdenMateriales")]
    public class OrdenMaterial
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

        [Display(Name = "Nº Línea")]
        public int NumeroLinea { get; set; }


        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArticulos", Controller = "Data")]
        [Display(Name = "Artículo")]
        public int ArticuloId { get; set; }
        [Display(Name = "Artículo")]
        public Articulo Articulo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public string Lote { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllUbicaciones", Controller = "Data")]
        [Display(Name = "Ubic. Origen")]
        public int UbicacionOrigenId { get; set; }
        [Display(Name = "Ubic. Origen")]
        public Ubicacion UbicacionOrigen { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllUbicaciones", Controller = "Data")]
        [Display(Name = "Ubic. Destino")]
        public int UbicacionDestinoId { get; set; }
        [Display(Name = "Ubic. Destino")]
        public Ubicacion UbicacionDestino { get; set; }

        public int Cantidad { get; set; }

        [Display(Name = "Cant. Real")]
        public int CantidadReal { get; set; }

        [Display(Name = "Mermas Teo.")]
        public int MermasTeoricas { get; set; }

        [Display(Name = "Mermas Real")]
        public int MermasReales { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string CodigoArticulo { get; set; }

    }
}

using LPFactory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Omu.AwesomeMvc;
using System.ComponentModel;

namespace LPFactory.Data
{
    [Table("Ordenes")]
    public class Orden
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArticulos", Controller = "Data")]
        [DisplayName("Articulo")]
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public int Prioridad { get; set; }

        public int Cantidad { get; set; }

        public int MermasTeoricas { get; set; }

        public int MermasReales { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllUbicaciones", Controller = "Data")]
        [DisplayName("Ubicación")]
        public int? UbicacionDestinoId { get; set; }
        public Ubicacion UbicacionDestino { get; set; }

        public int Producido { get; set; }

        public int Pendiente { get; set; }

        public OrdenEstado Estado { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public string Observaciones { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string Lote { get; set; }

        public string CodigoYDescripcion
        {
            get
            {
                return $"{Codigo} {Descripcion}";
            }
        }
    }
}

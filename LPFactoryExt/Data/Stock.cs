using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Omu.AwesomeMvc;

namespace LPFactory.Data
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }

        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllAlmacenes", Controller = "Data")]
        [Display(Name = "Almacén")]
        public int AlmacenId { get; set; }
        [Display(Name = "Almacén")]
        public Almacen Almacen { get; set; }

        [Display(Name = "Código Almacén")]
        public string CodigoAlmacen { get; set; }

        [Display(Name = "Descripción Almacén")]
        public string DescripcionAlmacen { get; set; }


        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllUbicaciones", Controller = "Data")]
        [Display(Name = "Ubicación")]
        public int UbicacionId { get; set; }
        [Display(Name = "Ubicación")]
        public Ubicacion Ubicacion { get; set; }

        [Display(Name = "Descripción Ubicación")]
        public string DescripcionUbicacion { get; set; }

        public string X { get; set; }

        public string Y { get; set; }

        public string Z { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArticulos", Controller = "Data")]
        [Display(Name = "Artículo")]
        public int ArticuloId { get; set; }
        [Display(Name = "Artículo")]
        public Articulo Articulo { get; set; }

        [Display(Name = "Código Artículo")]
        public string CodigoArticulo { get; set; }

        [Display(Name = "Descripción Artículo")]
        public string DescripcionArticulo { get; set; }

        public string Lote { get; set; }

        public string Referencia { get; set; }

        public int Cantidad { get; set; }


        public async Task RefrescaStock(ApplicationDbContext c)
        {
            var st = await c.Stocks
                .Include(s => s.Almacen)
                .Include(s => s.Articulo)
                .Include(s => s.Empresa)
                .Include(s => s.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == Id);

            CodigoAlmacen = st.Almacen.Codigo;
            DescripcionAlmacen = st.Almacen.Descripcion;
            DescripcionUbicacion = st.Ubicacion.Descripcion;
            X = st.Ubicacion.X;
            Y = st.Ubicacion.Y;
            Z = st.Ubicacion.Z;
            CodigoArticulo = st.Articulo.Codigo;
            DescripcionArticulo = st.Articulo.Descripcion;
            Lote = st.Articulo.Lote;
            Referencia = st.Articulo.Referencia;
        }

    }
}

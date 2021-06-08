using LPFactory.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Models
{
    public class ProductividadViewModel
    {
        public ProductividadViewModel ()
        {
            Produccion = new LineaResultado();
            Mermas = new LineaResultado();
            TiempoPreparacion = new LineaResultado();
            TiempoProduccion = new LineaResultado();
            NumeroIncidencias = new LineaResultado();
            TiempoIncidencias = new LineaResultado();
            NumeroMicroparadas = new LineaResultado();
            TiempoMicroparadas = new LineaResultado();
            Disponibilidad = new LineaResultado();
            Rendimiento = new LineaResultado();
            Calidad = new LineaResultado();
            OEE = new LineaResultado();

            LineasDatos = new List<LineaDatos>();
            Ordenes = new List<Orden>();
            Secciones = new List<Seccion>();
            Maquinas = new List<Maquina>();
            Operarios = new List<Operario>();
            Fases = new List<Fase>();
            Articulos = new List<Articulo>();
            ArticuloFamilias = new List<ArticuloFamilia>();
        }

        #region Filtros
        public int LineaDesde { get; set; }
        public int LineaHasta { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public DateTime FechaDesdeAnterior { get; set; }
        public DateTime FechaHastaAnterior { get; set; }
        public int SeccionDesde { get; set; }
        public int SeccionHasta { get; set; }
        public int OrdenDesde { get; set; }
        public int OrdenHasta { get; set; }
        public int Planta { get; set; }
        public int ArticuloId { get; set; }
        public int MaquinaDesde { get; set; }
        public int MaquinaHasta { get; set; }
        public int OperarioIdDesde { get; set; }
        public int OperarioIdHasta { get; set; }
        public int FaseDesde { get; set; }
        public int FaseHasta { get; set; }
        public int Estado { get; set; }
        public int FamiliaId { get; set; }

        #endregion

        #region Resultados

        public LineaResultado Produccion { get; set; }
        public LineaResultado Mermas { get; set; }
        public LineaResultado TiempoPreparacion { get; set; }
        public LineaResultado TiempoProduccion { get; set; }
        public LineaResultado NumeroIncidencias { get; set; }
        public LineaResultado TiempoIncidencias { get; set; }
        public LineaResultado NumeroMicroparadas { get; set; }
        public LineaResultado TiempoMicroparadas { get; set; }
        public LineaResultado Disponibilidad { get; set; }
        public LineaResultado Rendimiento { get; set; }
        public LineaResultado Calidad { get; set; }
        public LineaResultado OEE { get; set; }
        #endregion

        #region Grafico1
        public string DisponibilidadActual { get; set; }
        public string DisponibilidadAnterior { get; set; }
        public string RendimientoActual { get; set; }
        public string RendimientoAnterior { get; set; }
        public string CalidadActual { get; set; }
        public string CalidadAnterior { get; set; }
        public string OEEActual { get; set; }
        public string OEEAnterior { get; set; }

        #endregion

        public List<LineaDatos> LineasDatos { get; set; }
        public List<LineaDatosCompleta> LineasDatosCompleta { get; set; }
        public List<Orden> Ordenes { get; set; }
        public List<Seccion> Secciones { get; set; }
        public List<Maquina> Maquinas { get; set; }
        public List<Operario> Operarios { get; set; }
        public List<Fase> Fases { get; set; }
        public List<Articulo> Articulos { get; set; }
        public List<ArticuloFamilia> ArticuloFamilias { get; set; }
    }

    public class LineaResultado
    {
        public decimal Teorico { get; set; }
        public decimal Real { get; set; }
        public decimal RealAnt { get; set; }
    }

    public class LineaDatos
    {
        public string Orden { get; set; }
        public string Articulo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int Producido { get; set; }
        public int TiempoPreparacionTeorico { get; set; }
        public int TiempoPreparacionReal { get; set; }
        public int TiempoProduccionTeorico { get; set; }
        public int TiempoProduccionReal { get; set; }
        public int Mermas { get; set; }

    }

    public class LineaDatosCompleta
    {
        public int Id { get; set; }
        public string Orden { get; set; }
        public string Articulo { get; set; }
        public string Descripcion { get; set; }
        public string NumeroLote { get; set; }
        public string Operario { get; set; }
        public string Seccion { get; set; }
        public string Fase { get; set; }
        public string Operacion { get; set; }
        public string Maquina { get; set; }
        public int Secuencia { get; set; }
        public int TiempoPreparacion { get; set; }
        public int TiempoProduccion { get; set; }
        public int Cantidad { get; set; }
        public int MermasTeoricas { get; set; }
        public string FechaInicio { get; set; }
        public int TiempoPreparacionAcumulado { get; set; }
        public int TiempoProduccionAcumulado { get; set; }
        public int CantidadAcumulada { get; set; }
        public bool Abierta { get; set; }
        public bool Cerrada { get; set; }
        public int Mermas { get; set; }
        public int NumeroIncidencias { get; set; }
        public int TiempoIncidencias { get; set; }
        public int Actividad { get; set; }
        public int NumeroMicroparadas { get; set; }
        public int TiempoMicroparadas { get; set; }
        public string FechaFin { get; set; }
        public double DisponibilidadTeorica { get; set; }
        public double DisponibilidadReal { get; set; }
        public double Disponibilidad { get; set; }
        public double RendimientoTeorico { get; set; }
        public double RendimientoReal { get; set; }
        public double Rendimiento { get; set; }
        public double CalidadTeorica { get; set; }
        public double CalidadReal { get; set; }
        public double Calidad { get; set; }
        public double OEE { get; set; }

    }

    
}

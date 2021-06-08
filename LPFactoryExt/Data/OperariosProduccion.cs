using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("OperariosProduccion")]
    public class OperariosProduccion
    {
        public int Id { get; set; }

        public int OperarioId { get; set; }
        public Operario Operario { get; set; }

        public int? LineaId { get; set; }
        public Linea Linea { get; set; }

        public int? MaquinaId { get; set; }
        public Maquina Maquina { get; set; }

        public int? PuestoId { get; set; }
        public Puesto Puesto { get; set; }

        public DateTime FechaInicio { get; set; }

        public int TiempoProduccionIni { get; set; }
        
        public int TiempoPreparacionIni { get; set; }
        
        public int TiempoIncidenciaIni { get; set; }
        
        public int TiempoMicroparadasIni { get; set; }
        
        public int NumeroIncidenciasIni { get; set; }
        
        public int NumeroMicroparadasIni { get; set; }
        
        public int CantidadIni { get; set; }
        
        public int MermasIni { get; set; }
        
        public int? TiempoProduccionFin { get; set; }
        
        public int? TiempoPreparacionFin { get; set; }
        
        public int? TiempoIncidenciasFin { get; set; }
        
        public int? TiempoMicroparadasFin { get; set; }
        
        public int? NumeroIncidenciasFin { get; set; }
        
        public int? NumeroMicroparadasFin { get; set; }
        
        public int? CantidadFin { get; set; }
        
        public int? MermasFin { get; set; }
        
        public int Estado { get; set; }
        
        public DateTime? FechaFin { get; set; }

    }
}

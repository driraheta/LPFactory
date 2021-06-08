using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Models
{
    /// <summary>
    /// Estado de las órdenes y escandallos
    /// </summary>
    public enum OrdenEstado
    {
        /// <summary>
        /// Pendiente de lanzamiento, no se puede fabricar todavía
        /// </summary>
        [Display(Name = "Pendiente")]
        Pendiente = 0,
        /// <summary>
        /// Lanzada y lista para producción
        /// </summary>
        [Display(Name = "Lanzada")]
        Lanzada = 1,
        /// <summary>
        /// En proceso
        /// </summary>
        [Display(Name = "En Proceso")]
        Proceso = 2,
        /// <summary>
        /// En pausa o interrumpida, lista para volver a abrirla
        /// </summary>
        [Display(Name = "En Pausa")]
        Pausa = 3,
        /// <summary>
        /// Cerrada o terminada
        /// </summary>
        [Display(Name = "Cerrada")]
        Cerrada = 4
    }
}

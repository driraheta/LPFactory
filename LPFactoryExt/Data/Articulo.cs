using Microsoft.AspNetCore.Mvc;
using Omu.AwesomeMvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Articulos")]
    public class Articulo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public int Actividad { get; set; }

        public bool Activo { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllFamiliasArticulos", Controller = "Data")]
        [Display(Name = "Familia")]
        public int ArticuloFamiliaId { get; set; }
        public ArticuloFamilia Familia { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllTipoArticulos", Controller = "Data")]
        [Display(Name = "Tipo")]
        public int ArticuloTipoId { get; set; }
        public ArticuloTipo Tipo { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllUbicaciones", Controller = "Data")]
        [Display(Name = "Ubicación")]
        public int UbicacionId { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public string Lote { get; set; }

        public int Stock { get; set; }

        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string Referencia { get; set; }

        [Display(Name = "Código de barras")]
        public string CodigoBarras { get; set; }

        public string CodigoYDescripcion
        {
            get
            {
                return $"{Codigo} {Descripcion}";
            }
        }

        public string CodigoYDescripcionCompleta
        {
            get
            {
                return $"Código: {Codigo,-10} Desc: {Descripcion,50} Lote: {Lote} Ref: {Referencia} ";
            }
        }
    }

}

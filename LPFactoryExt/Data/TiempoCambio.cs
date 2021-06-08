using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Omu.AwesomeMvc;

namespace LPFactory.Data
{
    [Table("TiemposCambio")]
    public class TiempoCambio
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArgumentos", Controller = "Data")]        
        [Display(Name = "Argumento")]
        public int ArgumentoId { get; set; }
        public Argumento Argumento { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArgumentoValor", Controller = "Data")]
        [Display(Name = "Valor Actual")]
        public int ValorActualId { get; set; }
        [Display(Name = "Valor Actual")]
        public ArgumentoValor ValorActual { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllArgumentoValor", Controller = "Data")]
        [Display(Name = "Valor Siguiente")]
        public int ValorSiguienteId { get; set; }
        [Display(Name = "Valor Siguiente")]
        public ArgumentoValor ValorSiguiente { get; set; }

        public int Tiempo { get; set; }

    }
}

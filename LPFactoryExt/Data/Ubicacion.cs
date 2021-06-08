using System.ComponentModel.DataAnnotations.Schema;
using Omu.AwesomeMvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace LPFactory.Data
{
    [Table("Ubicaciones")]
    public class Ubicacion
    {
        public int Id { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllAlmacenes", Controller = "Data")]
        [DisplayName("Almacen")]
        public int AlmacenId { get; set; }
        public Almacen Almacen { get; set; }

        public string Descripcion { get; set; }

        public string X { get; set; }
        
        public string Y { get; set; }
        
        public string Z { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

    }
}

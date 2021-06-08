using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Recursos")]
    public class Recurso
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public byte[] Binario { get; set; }

        public TipoRecurso Tipo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }

    public enum TipoRecurso
    {
        Imagen = 0,
        Archivo = 1,
        Logo = 2,
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    [Table("Empresas")]
    public class Empresa
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public string Direccion { get; set; }

        public string NombreERP { get; set; }

        [Display(Name = "Logo")]
        public int? LogoId { get; set; }


        public ICollection<Articulo> Articulos{ get; set; }
        
        public ICollection<Argumento> Argumentos { get; set; }
        
        public ICollection<Maquina> Maquinas { get; set; }

        public ICollection<Operacion> Operaciones { get; set; }
        
        public ICollection<Ubicacion> Ubicaciones { get; set; }

        public static async Task<Empresa> GetEmpresaFromUserId(string userId, ApplicationDbContext context)
        {
            var query = from u in context.Users
                        from e in context.Empresas
                        where u.Id == userId
                        where u.EmpresaId == e.Id
                        select e;
            return await query.FirstAsync();

        }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPFactory.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

    }
}

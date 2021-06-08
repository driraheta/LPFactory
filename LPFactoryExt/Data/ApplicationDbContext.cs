using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LPFactory.Data;
using LPFactory.Models;

namespace LPFactory.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Empresa>().HasData(new Empresa { Id = 1, Nombre = "Tinte de los Alemanes", Alta = true });

            builder.Entity<Almacen>()
                .HasIndex(x => x.Codigo)
                .IsUnique();
            builder.Entity<Almacen>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Articulo>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<ArticuloFamilia>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Empresa>()
                .HasIndex(x => x.Codigo)
                .IsUnique();
            builder.Entity<Empresa>()
                .Property(x => x.Activo)
                .HasDefaultValue(false);

            builder.Entity<Fase>()
                .HasIndex(x => x.Codigo)
                .IsUnique();
            builder.Entity<Fase>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Incidencia>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<IncidenciaTipo>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Maquina>()
                .HasIndex(x => x.Codigo)
                .IsUnique();
            builder.Entity<Maquina>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Operacion>()
                .HasIndex(x => x.Codigo)
                .IsUnique();
            builder.Entity<Operacion>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Operario>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Orden>()
                .Property(x => x.Fecha)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Orden>()
                .Property(x => x.Prioridad)
                .HasDefaultValue(0);
            builder.Entity<Orden>()
                .Property(x => x.MermasReales)
                .HasDefaultValue(0);
            builder.Entity<Orden>()
                .Property(x => x.Producido)
                .HasDefaultValue(0);
            builder.Entity<Orden>()
                .Property(x => x.Pendiente)
                .HasDefaultValue(0);
            builder.Entity<Orden>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<OrdenOperacion>()
                .Property(x => x.TiempoPreparacionReal)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.TiempoProduccionReal)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.CantidadProducida)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.MermasReales)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.TiempoIncidencias)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.NumeroIncidencias)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.TiempoMicroparadas)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.NumeroMicroparadas)
                .HasDefaultValue(0);
            builder.Entity<OrdenOperacion>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<OrdenMaterial>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Planta>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

            builder.Entity<Seccion>()
                .HasIndex(x => x.Codigo)
                .IsUnique();
            builder.Entity<Seccion>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);
            builder.Entity<Seccion>()
                .Property(x => x.PlantaId)
                .HasDefaultValue(1);

            builder.Entity<Ubicacion>()
                .Property(x => x.EmpresaId)
                .HasDefaultValue(1);

        }

        public DbSet<Almacen> Almacenes { get; set; }
        
        public DbSet<Argumento> Argumentos { get; set; }
        
        public DbSet<ArgumentoValor> ArgumentoValores{ get; set; }

        public DbSet<Articulo> Articulos { get; set; }

        public DbSet<ArticuloFamilia> ArticuloFamilias { get; set; }

        public DbSet<ArticuloTipo> ArticuloTipos { get; set; }

        public DbSet<Empresa> Empresas { get; set; }
        
        public DbSet<Estructura> Estructuras { get; set; }
        
        public DbSet<EstructuraFase> EstructurasFases { get; set; }
        
        public DbSet<EstructuraFaseMaterial> EstructurasFasesMateriales { get; set; }
        
        public DbSet<EstructuraFaseOperacion> EstructuraFaseOperaciones { get; set; }

        public DbSet<Fase> Fases { get; set; }

        public DbSet<Incidencia> Incidencias { get; set; }

        public DbSet<IncidenciaTipo> IncidenciaTipos { get; set; }

        public DbSet<Linea> Lineas { get; set; }

        public DbSet<Maquina> Maquinas { get; set; }

        public DbSet<Operacion> Operaciones { get; set; }

        public DbSet<Operario> Operarios { get; set; }

        public DbSet<OperariosProduccion> OperariosProduccion { get; set; }

        public DbSet<Orden> Ordenes { get; set; }

        public DbSet<OrdenOperacion> OrdenOperaciones { get; set; }

        public DbSet<OrdenMaterial> OrdenMateriales { get; set; }

        public DbSet<Planta> Plantas { get; set; }

        public DbSet<Puesto> Puestos { get; set; }
        
        public DbSet<Recurso> Recursos { get; set; }

        public DbSet<Seccion> Secciones { get; set; }
        
        public DbSet<Stock> Stocks { get; set; }
        
        public DbSet<TiempoCambio> TiemposCambio { get; set; }

        public DbSet<Ubicacion> Ubicaciones { get; set; }

        public DbSet<ApplicationUser> Usuarios { get; set; }

        public DbSet<LPFactory.Models.EscandalloViewModel> EscandalloViewModel { get; set; }

    }
}

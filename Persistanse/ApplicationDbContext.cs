using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Model;
using Model.Domain;
using System.Linq;

namespace Persistanse
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRol> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<RolEmp> RolEmpleado { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<UserPorEmp> UserPorEmp { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        public DbSet<Recurso> Recursos { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

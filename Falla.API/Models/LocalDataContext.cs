using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Falla.API.Models
{
    public class LocalDataContext : DbContext
    {
        public LocalDataContext() : base("DefaultConnection")
        {
        }

        //public DbSet<Evento> Eventos { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Componente> Componentes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
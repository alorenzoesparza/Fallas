using Fallas.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Falla.API.Models
{
    public class LocalDataContext : DataContext
    {
        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Componente> Componentes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
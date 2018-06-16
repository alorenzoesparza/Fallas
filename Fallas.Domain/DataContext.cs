using System.Data.Entity;

namespace Fallas.Domain
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Fallas.Domain.Componente> Componentes { get; set; }
    }
}

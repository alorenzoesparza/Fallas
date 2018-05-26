using System.Data.Entity;

namespace Fallas.Domain
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Fallas.Domain.Act> Acts { get; set; }
    }
}

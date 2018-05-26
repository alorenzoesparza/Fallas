using Fallas.Domain;
using System.Data.Entity;

namespace Fallas.Backend.Models
{
    public class LocalDataContext : DataContext
    {
        public DbSet<Component> Components { get; set; }

        public DbSet<Act> Acts { get; set; }
    }
}
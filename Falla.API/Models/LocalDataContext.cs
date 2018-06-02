using Fallas.Domain;
using System.Data.Entity;

namespace Falla.API.Models
{
    public class LocalDataContext : DataContext
    {
        public DbSet<Component> Components { get; set; }

        public DbSet<Act> Acts { get; set; }
    }
}
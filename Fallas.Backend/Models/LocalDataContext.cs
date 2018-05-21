using Fallas.Domain;

namespace Fallas.Backend.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Fallas.Domain.Component> Components { get; set; }
    }
}
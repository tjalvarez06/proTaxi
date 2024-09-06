

using Microsoft.EntityFrameworkCore;
using proTaxi.Domain.Entities;

namespace proTaxi.Persistence.Context
{
    public class TaxiDb : DbContext
    {
         public TaxiDb(DbContextOptions<TaxiDb> options) : base(options) 
        {

        }
        public DbSet<Taxi> Taxi { get; set; }
        public DbSet<Viaje> Viaje { get; set; }
    }
}

using Governement_Public_Health_Care.Models;
using Microsoft.EntityFrameworkCore;

namespace Governement_Public_Health_Care.DB_Context
{
    public class HealthCareContext : DbContext
    {
        //Table names for our database
        public DbSet<DiseaseRegistry> DiseaseRegistries { get; set; }

        public HealthCareContext(DbContextOptions Options) : base(Options) { }
    }
}

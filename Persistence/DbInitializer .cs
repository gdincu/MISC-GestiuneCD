using GestiuneCD.Models.Entities;
using GestiuneCD.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GestiuneCD.Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.Migrate();
        }

        public void SeedData()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            //add admin user
            if (!context.CDs.Any())
            {
                var adminUser = new CD
                {
                    Nume = "Test1",
                    Tip = TipCD.CDRW,
                    NrDeSesiuni = 0,
                    DimensiuneMB = 700,
                    SpatiuOcupat = 100,
                    VitezaMaxInscriptionare = VitezaInscriptionare.x4
                };
                context.CDs.Add(adminUser);
            }

            context.SaveChanges();
        }
    }
}

﻿using GestiuneCD.Domain;
using GestiuneCD.Models;
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
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {

                    //add admin user
                    if (!context.CDs.Any())
                    {
                        var adminUser = new CD
                        {
                            nume = "Test1",
                            tip = TipCD.CDRW,
                            tipSesiune = TipSesiune.Null,
                            nrDeSesiuni = 0,
                            dimensiuneMB = 700,
                            spatiuOcupat = 100,
                            vitezaDeInscriptionare = 11
                        };
                        context.CDs.Add(adminUser);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

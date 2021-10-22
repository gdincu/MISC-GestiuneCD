﻿using GestiuneCD.Domain;
using GestiuneCD.Models;
using GestiuneCD.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiuneCD.Persistence
{
    public class CDService : ICDService<CD>
    {
        private readonly AppDbContext _context;

        public CDService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<CD>> CreateItemAsync(CDSetupDTO entity)
        {
            int dimensiuneMB = (entity.tip == TipCD.CDDA) ? 804 : 700;

            if (entity.spatiuOcupat > dimensiuneMB)
                throw new Exception("Spatiul ocupat nu poate depasi capacitatea acestui tip de CD!");

            CD tempCD = new CD(entity.nume, dimensiuneMB, entity.vitezaMaxInscriptionare, entity.tip, entity.spatiuOcupat, 0);

            _context.CDs.Add(tempCD);
            await _context.SaveChangesAsync();
            return tempCD;
        }

        public async Task<ActionResult<CD>> DeleteItemAsync(int id)
        {
            if (!CDExists(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            CD tempCD = await _context.FindAsync<CD>(id);
            _context.Remove(tempCD);
            await _context.SaveChangesAsync();
            return tempCD;
        }

        public async Task<CD> GetItemByIdAsync(int id)
        {
            if (!CDExists(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            return await _context.FindAsync<CD>(id);
        }

        public async Task<IEnumerable<CD>> GetItemsAsync(bool? orderedByName = false, bool? orderedBySize = false, int? minSpatiuLiber = 0, VitezaInscriptionare? vitezaDeInscriptionare = null, TipCD? tipCD = null, bool? cuSesiuniDeschise = null)
        {
            //Lista initiala de CD-uri ce trebuie filtrata
            IQueryable<CD> result = _context.CDs;

            if (orderedByName == true)
                result = result.OrderBy(f => f.nume);

            if (orderedBySize == true)
                result = result.OrderBy(f => f.dimensiuneMB);

            if (minSpatiuLiber > 0)
                result = result.Where(f => (f.dimensiuneMB - f.spatiuOcupat) >= minSpatiuLiber);

            if (vitezaDeInscriptionare is not null)
                result = result.Where(f => f.vitezaMaxInscriptionare.Equals(vitezaDeInscriptionare));

            if (tipCD is not null)
                result = result.Where(f => f.tip == tipCD);

            switch(cuSesiuniDeschise) {
                case true:
                    result = IntoarceCDuriInFunctieDeSesiunileDeschise(result,StatusSesiune.Deschis);
                    break;
                case false:
                    result = IntoarceCDuriInFunctieDeSesiunileDeschise(result,StatusSesiune.Inchis);
                    break;
                default:
                    break;
            }

            return await result.ToListAsync();
        }

        //Filtreaza o lista cu CD-uri pentru a mentine doar CD-urile cu sesiuni deschise sau nu in functie de statusSesiune primit
        private IQueryable<CD> IntoarceCDuriInFunctieDeSesiunileDeschise(IQueryable<CD> initialList,StatusSesiune statusSesiune)
        {
            var listaSesiuniDeschise = _context.Sesiuni
                .Where(f => f.statusSesiune.Equals(StatusSesiune.Deschis))
                .Select(f => f.idCD);

            if(statusSesiune.Equals(StatusSesiune.Deschis))
                return initialList.Where(f => listaSesiuniDeschise.Contains(f.id));
            else
                return initialList.Where(f => !listaSesiuniDeschise.Contains(f.id));
        }

        public async Task<ActionResult<CD>> UpdateItemAsync(int id, CDUpdateDTO entity)
        {
            if (!CDExists(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            CD retrievedCD = await _context.CDs.FirstOrDefaultAsync(f => f.id == id);

            CD tempCD = new CD(entity.nume,
                                retrievedCD.dimensiuneMB,
                                entity.vitezaMaxInscriptionare,
                                retrievedCD.tip,
                                retrievedCD.spatiuOcupat,
                                retrievedCD.nrDeSesiuni
                                );
            tempCD.id = id;

            _context.Entry(retrievedCD).CurrentValues.SetValues(tempCD);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return tempCD;
        }

        private bool CDExists(int id)
        {
            return _context.CDs.Any(e => e.id == id);
        }
    }
}

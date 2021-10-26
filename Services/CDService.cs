using GestiuneCD.Models.DTOs;
using GestiuneCD.Models.Entities;
using GestiuneCD.Models.Enums;
using GestiuneCD.Persistence;
using GestiuneCD.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestiuneCD.Services
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
            int dimensiuneMB = (entity.Tip == TipCD.CDDA) ? 804 : 700;

            if (entity.SpatiuOcupat > dimensiuneMB)
                throw new Exception("Spatiul ocupat nu poate depasi capacitatea acestui tip de CD!");

            CD tempCD = new(entity.Nume, dimensiuneMB, entity.VitezaMaxInscriptionare, entity.Tip, entity.SpatiuOcupat, 0);

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
                result = result.OrderBy(f => f.Nume);

            if (orderedBySize == true)
                result = result.OrderBy(f => f.DimensiuneMB);

            if (minSpatiuLiber > 0)
                result = result.Where(f => (f.DimensiuneMB - f.SpatiuOcupat) >= minSpatiuLiber);

            if (vitezaDeInscriptionare is not null)
                result = result.Where(f => f.VitezaMaxInscriptionare.Equals(vitezaDeInscriptionare));

            if (tipCD is not null)
                result = result.Where(f => f.Tip == tipCD);

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
                .Where(f => f.StatusSesiune.Equals(StatusSesiune.Deschis))
                .Select(f => f.IdCD);

            if(statusSesiune.Equals(StatusSesiune.Deschis))
                return initialList.Where(f => listaSesiuniDeschise.Contains(f.ID));
            else
                return initialList.Where(f => !listaSesiuniDeschise.Contains(f.ID));
        }

        public async Task<ActionResult<CD>> UpdateItemAsync(int id, CDUpdateDTO entity)
        {
            if (!CDExists(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            CD retrievedCD = await _context.CDs.FirstOrDefaultAsync(f => f.ID == id);

            CD tempCD = new(entity.Nume,
                                retrievedCD.DimensiuneMB,
                                entity.VitezaMaxInscriptionare,
                                retrievedCD.Tip,
                                retrievedCD.SpatiuOcupat,
                                retrievedCD.NrDeSesiuni
                                );
            tempCD.ID = id;

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
            return _context.CDs.Any(e => e.ID == id);
        }
    }
}

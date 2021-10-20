using GestiuneCD.Domain;
using GestiuneCD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestiuneCD.Persistence
{
    public class CDsService : ICDsService<CD>
    {
        private readonly AppDbContext _context;

        public CDsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<CD>> CreateItemAsync(CDSetupDTO entity)
        {
            int dimensiuneMB = (entity.tip == TipCD.CDDA) ? 804 : 700;

            if (entity.spatiuOcupat > dimensiuneMB)
                throw new Exception("Spatiul ocupat nu poate depasi capacitatea acestui tip de CD!");

            int nrDeSesiuni = (entity.tipSesiune == TipSesiune.Null) ? 0 : 1;

            CD tempCD = new CD(entity.nume, dimensiuneMB, entity.vitezaDeInscriptionare, entity.tip, entity.spatiuOcupat, nrDeSesiuni, entity.tipSesiune);

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

        public async Task<IEnumerable<CD>> GetItemsAsync(bool? orderedByName = false, int? minSpatiuLiber = 0, int? vitezaDeInscriptionare = 0, TipCD? tipCD = null)
        {
            IQueryable<CD> result = _context.CDs;

            if (orderedByName == true)
                result = result.OrderBy(f => f.nume);

            if (minSpatiuLiber > 0)
                result = result.Where(f => (f.dimensiuneMB - f.spatiuOcupat) >= minSpatiuLiber);

            if (vitezaDeInscriptionare > 0)
                result = result.Where(f => f.vitezaDeInscriptionare == vitezaDeInscriptionare);

            if (tipCD != null)
                result = result.Where(f => f.tip == tipCD);

            return await result.ToListAsync();
        }

        public async Task<ActionResult<CD>> UpdateItemAsync(int id, CDUpdateDTO entity)
        {
            if (!CDExists(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            CD retrievedCD = await _context.CDs.FirstOrDefaultAsync(f => f.id == id);

            if (retrievedCD.tip != TipCD.CDRW
                && entity.tipSesiune == TipSesiune.Scriere
                && retrievedCD.nrDeSesiuni > 0)
                throw new Exception("Tipul acesta de CD nu permite rescrierea datelor!");

            if (entity.spatiuOcupatAditional + retrievedCD.spatiuOcupat > retrievedCD.dimensiuneMB
                && entity.tipSesiune.Equals(TipSesiune.Scriere))
                throw new Exception("Aceasta operatiune ar rezulta in dimensiunea maxima a acestui CD sa fie depasita!");

            decimal spatiuOcupatAditional = (entity.tipSesiune == TipSesiune.Scriere) ? entity.spatiuOcupatAditional : 0;
            int nrDeSesiuniAditionale = (entity.tipSesiune != TipSesiune.Null) ? 1 : 0;

            CD tempCD = new CD(entity.nume,
                                retrievedCD.dimensiuneMB,
                                entity.vitezaDeInscriptionare,
                                retrievedCD.tip,
                                spatiuOcupatAditional + retrievedCD.spatiuOcupat,
                                retrievedCD.nrDeSesiuni + nrDeSesiuniAditionale,
                                entity.tipSesiune
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

        public async Task<IEnumerable<CD>> OrderBySize(string? orderType)
        {
            List<CD> OrderedList = new List<CD>();

            switch (orderType)
            {
                case "ASC":
                    OrderedList = _context.CDs.OrderBy(f => f.dimensiuneMB).ToList();
                    break;

                case "DESC":
                    OrderedList = _context.CDs.OrderByDescending(f => f.dimensiuneMB).ToList();
                    break;

                default:
                    OrderedList = _context.CDs.ToList();
                    break;
            }

            foreach (var item in _context.CDs)
                _context.CDs.Remove(item);

            foreach (var item in OrderedList)
                _context.CDs.Add(new CD(item.nume, item.dimensiuneMB, item.vitezaDeInscriptionare, item.tip, item.spatiuOcupat, item.nrDeSesiuni, item.tipSesiune));

            await _context.SaveChangesAsync();

            return await _context.CDs.ToListAsync();
        }

        private bool CDExists(int id)
        {
            return _context.CDs.Any(e => e.id == id);
        }
    }
}

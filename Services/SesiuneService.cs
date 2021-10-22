using GestiuneCD.Domain;
using GestiuneCD.Models;
using GestiuneCD.Models.DTOs;
using GestiuneCD.Models.Entities;
using GestiuneCD.Models.Enums;
using GestiuneCD.Persistence;
using GestiuneCD.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestiuneCD.Services
{
    public class SesiuneService : ISesiuneService<Sesiune>
    {
        private readonly AppDbContext _context;

        public SesiuneService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Sesiune>> CreateItemAsync(SesiuneSetupDTO entity,decimal? spatiuAditionalOcupat)
        {
            if(!_context.CDs.Any(f => f.id == entity.idCD))
                throw new Exception("Id-ul introdus nu exista in baza de date!");
            
            //Daca exista sesiuni deschise pentru CD-ul ales si vrem sa deschidem o noua sesiune primim eroarea de mai jos
            if (_context.Sesiuni.Any(f => f.idCD == entity.idCD && f.statusSesiune.Equals(StatusSesiune.Deschis)))
                throw new Exception("Exista deja macar o sesiune deschisa pentru acest CD!");

            if (entity.tipSesiune.Equals(TipSesiune.Scriere) && (!spatiuAditionalOcupat.HasValue || spatiuAditionalOcupat<=0))
                throw new Exception("Spatiu aditional ocupat trebuie completat pentru acest tip de sesiune!");

            if(entity.tipSesiune.Equals(TipSesiune.Scriere) && (entity.vitezaInscriptionare == null))
                throw new Exception("O viteza de inscriptionare trebuie completata pentru acest tip de sesiune!");

            CD CDVizat = await _context.CDs.FirstOrDefaultAsync(f => f.id == entity.idCD);

            //Un CD ce nu este CD-RW nu se poate scrie de mai multe ori
            if (entity.tipSesiune.Equals(TipSesiune.Scriere) && (!CDVizat.tip.Equals(TipCD.CDRW)))
                throw new Exception("Doar CD-urile de tip CD-RW se pot rescrie!");

            if (entity.vitezaInscriptionare > CDVizat.vitezaMaxInscriptionare)
                throw new Exception("Viteza de inscriptionare nu poate depasi viteza maxima a CD-ului!");

            if(entity.tipSesiune.Equals(TipSesiune.Scriere) && (spatiuAditionalOcupat > (CDVizat.dimensiuneMB - CDVizat.spatiuOcupat)))
                throw new Exception("Memorie insuficienta! CD-ul mai are doar " + (CDVizat.dimensiuneMB - CDVizat.spatiuOcupat) + " Mb liberi!");

            VitezaInscriptionare? vitezaInscriptionare = null;
            if (entity.tipSesiune.Equals(TipSesiune.Scriere))
                vitezaInscriptionare = entity.vitezaInscriptionare;

            Sesiune sesiuneNoua = new Sesiune(CDVizat, entity.idCD, DateTime.Now, null, entity.tipSesiune, vitezaInscriptionare,StatusSesiune.Deschis);

            try
            {
                //Adauga noua sesiune
                _context.Sesiuni.Add(sesiuneNoua);

                //Incrementeaza nr de sesiuni pentru CD-ul vizat
                CD CDNou = CDVizat;
                CDNou.nrDeSesiuni++;
                //Adauga spatiul ocupat aditional unde este cazul (eg. tip sesiune = scriere)
                CDNou.spatiuOcupat = (decimal)((sesiuneNoua.tipSesiune.Equals(TipSesiune.Scriere)) ? (CDNou.spatiuOcupat + spatiuAditionalOcupat) : CDNou.spatiuOcupat);
                _context.Entry(CDVizat).CurrentValues.SetValues(CDNou);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return sesiuneNoua;
        }

        public async Task<ActionResult<Sesiune>> DeleteItemAsync(int id)
        {
            if (!SesiuneaExista(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            Sesiune tempSesiune = await _context.FindAsync<Sesiune>(id);

            try {
                //Sterge sesiune
                _context.Remove(tempSesiune);

                //Decrementeaza nr de sesiuni
                CD CDVizat = await _context.CDs.FirstOrDefaultAsync(f => f.id == tempSesiune.idCD);
                CD CDNou = CDVizat;
                CDNou.nrDeSesiuni--;
                _context.Entry(CDVizat).CurrentValues.SetValues(CDNou);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return tempSesiune;
        }

        public async Task<Sesiune> GetItemByIdAsync(int id)
        {
            if (!SesiuneaExista(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            return await _context.FindAsync<Sesiune>(id);
        }

        public async Task<IEnumerable<Sesiune>> GetItemsAsync()
        {
            return await _context.Sesiuni.ToListAsync();
        }

        public async Task<ActionResult<Sesiune>> UpdateItemAsync(int id)
        {
            if (!SesiuneaExista(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            Sesiune sesiuneVizata = await _context.Sesiuni.FirstOrDefaultAsync(f => f.id == id);

            if(sesiuneVizata.statusSesiune.Equals(StatusSesiune.Inchis))
                throw new Exception("Sesiunea vizata este deja inchisa!");

            Sesiune sesiuneNoua = sesiuneVizata;
            sesiuneNoua.statusSesiune = StatusSesiune.Inchis;
            sesiuneNoua.endDateTime = DateTime.Now;
            
            try
            {
                _context.Entry(sesiuneVizata).CurrentValues.SetValues(sesiuneNoua);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return sesiuneNoua;
        }
        private bool SesiuneaExista(int id)
        {
            return _context.Sesiuni.Any(e => e.id == id);
        }

        public async Task<IEnumerable<Sesiune>> InchideSesiunileDeschise()
        {
            List<Sesiune> sesiuniDeschise = _context.Sesiuni.Where(f => f.statusSesiune.Equals(StatusSesiune.Deschis)).ToList();

            foreach (var item in sesiuniDeschise) {
                Sesiune tempSesiune = item;
                tempSesiune.statusSesiune = StatusSesiune.Inchis;
                tempSesiune.endDateTime = DateTime.Now;
                _context.Entry(item).CurrentValues.SetValues(tempSesiune);
            }

            await _context.SaveChangesAsync();

            return sesiuniDeschise;
        }
    }
}

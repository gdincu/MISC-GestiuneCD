using GestiuneCD.Domain;
using GestiuneCD.Models;
using GestiuneCD.Models.DTOs;
using GestiuneCD.Models.Entities;
using GestiuneCD.Models.Enums;
using GestiuneCD.Persistence;
using GestiuneCD.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiuneCD.Services
{
    public class SesiuneService : ISesiuneService<Sesiune>
    {
        private readonly AppDbContext _context;

        public SesiuneService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Sesiune>> CreateItemAsync(SesiuneSetupDTO entity)
        {
            if (_context.Sesiuni.Any(f => f.idCD == entity.idCD && f.statusSesiune.Equals(StatusSesiune.Deschis)) 
                && entity.statusSesiune.Equals(StatusSesiune.Deschis)
                                        )
                throw new Exception("Exista deja o sesiune deschisa pentru acest CD");

            bool sesiuneAditionala = !entity.tipSesiune.Equals(TipSesiune.Null);
            
            CD tempCD = await _context.CDs.FirstOrDefaultAsync(f => f.id == entity.idCD);

            //WIP
            DateTime? dateTimeStart = entity.statusSesiune.Equals(StatusSesiune.Deschis) ? DateTime.Now : null;

            //WIP
            DateTime? dateTimeEnd = entity.statusSesiune.Equals(StatusSesiune.Inchis) ? DateTime.Now : null;

            Sesiune tempSesiune = new Sesiune(tempCD, entity.idCD, dateTimeStart, dateTimeEnd, entity.tipSesiune, entity.statusSesiune);

            try
            {
                _context.Sesiuni.Add(tempSesiune);

                //Incrementeaza nr de sesiuni in functie de tipul de sesiune
                if (sesiuneAditionala && entity.statusSesiune.Equals(StatusSesiune.Deschis)) { 
                    CD newCD = tempCD;
                    newCD.nrDeSesiuni++;
                    _context.Entry(tempCD).CurrentValues.SetValues(newCD);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return tempSesiune;
        }

        public async Task<ActionResult<Sesiune>> DeleteItemAsync(int id)
        {
            if (!SesiuneaExista(id))
                throw new Exception("Id-ul introdus nu exista in baza de date!");

            Sesiune tempSesiune = await _context.FindAsync<Sesiune>(id);
            _context.Remove(tempSesiune);
            await _context.SaveChangesAsync();
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

        public Task<ActionResult<Sesiune>> UpdateItemAsync(int id, Sesiune entity)
        {
            throw new NotImplementedException();
        }
        private bool SesiuneaExista(int id)
        {
            return _context.Sesiuni.Any(e => e.id == id);
        }
    }
}

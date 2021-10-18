using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiuneCD.Domain;
using GestiuneCD.Persistence;

namespace GestiuneCD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CDsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CD>>> GetCDs(bool? orderByName = false, int? minSpatiuLiber = 0)
        {
            //Filters results by deadline
            IQueryable<CD> result = _context.CDs;

            if (orderByName == true)
                result = result.OrderBy(f => f.nume);

            if (minSpatiuLiber > 0)
                result = result.Where(f => (f.dimensiuneMB - f.spatiuOcupat) >= minSpatiuLiber);

            //return await _context.CDs.ToListAsync();
            return await result.ToListAsync();
        }

        // GET: api/CDs
        [HttpGet("/BasedOnAttributes")]
        public async Task<ActionResult<IEnumerable<CD>>> GetCDsByAttributes(int? vitezaDeInscriptionare = 0, string? tipCD = null)
        {
            //Filters results by deadline
            IQueryable<CD> result = _context.CDs;

            if (vitezaDeInscriptionare != 0)
                result = result.Where(f => f.vitezaDeInscriptionare == vitezaDeInscriptionare);

            if (tipCD != null)
                result = result.Where(f => f.tip.Equals(tipCD));

            //return await _context.CDs.ToListAsync();
            return await result.ToListAsync();
        }

        // GET: api/CDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CD>> GetCD(int id)
        {
            var cD = await _context.CDs.FindAsync(id);

            if (cD == null)
            {
                return NotFound();
            }

            return cD;
        }

        // PUT: api/CDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCD(int id, CD cD)
        {
            if (id != cD.id)
            {
                return BadRequest();
            }

            _context.Entry(cD).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CDExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CD>> PostCD(CD cD)
        {
            _context.CDs.Add(cD);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCD", new { id = cD.id }, cD);
            return CreatedAtAction(nameof(GetCD), new { id = cD.id }, cD);
        }

        // POST: Order api/CDs
        [HttpPost("/OrderBySize")]
        public async Task<ActionResult<IEnumerable<CD>>> OrderBySize(string? orderMethod = "ASC")
        {
            List<CD> OrderedList = new List<CD>();

            switch (orderMethod)
            {
                case "ASC":
                    OrderedList = _context.CDs.OrderBy(f => f.dimensiuneMB).ToList();
                    break;

                case "DESC":
                    OrderedList = _context.CDs.OrderByDescending(f => f.dimensiuneMB).ToList();
                    break;

                default:
                    break;
            }

            foreach (var item in _context.CDs)
                _context.CDs.Remove(item);

            foreach (var item in OrderedList)
                _context.CDs.Add(new CD(item.nume, item.dimensiuneMB, item.vitezaDeInscriptionare, item.tip, item.spatiuOcupat, item.nrDeSesiuni, item.tipSesiune));

            await _context.SaveChangesAsync();

            return await _context.CDs.ToListAsync();
        }

        // DELETE: api/CDs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCD(int id)
        {
            var cD = await _context.CDs.FindAsync(id);
            if (cD == null)
            {
                return NotFound();
            }

            _context.CDs.Remove(cD);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CDExists(int id)
        {
            return _context.CDs.Any(e => e.id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiuneCD.Domain;
using GestiuneCD.Persistence;
using GestiuneCD.Models;

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

        /// <summary>
        /// Returneaza toate CD-urile.
        /// </summary>
        // GET: api/CDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CD>>> GetCDs(bool? orderedByName = false, int? minSpatiuLiber = 0)
        {
            //Filters results by deadline
            IQueryable<CD> result = _context.CDs;

            if (orderedByName == true)
                result = result.OrderBy(f => f.nume);

            if (minSpatiuLiber > 0)
                result = result.Where(f => (f.dimensiuneMB - f.spatiuOcupat) >= minSpatiuLiber);

            //return await _context.CDs.ToListAsync();
            return await result.ToListAsync();
        }

        /// <summary>
        /// Returneaza datele tuturor CD-urilor bazate pe anumite atribute - viteza de inscriptionare, tipul de CD.
        /// </summary>
        // GET: api/CDs
        [HttpGet("/BasedOnAttributes")]
        public async Task<ActionResult<IEnumerable<CD>>> GetCDsByAttributes(int? vitezaDeInscriptionare = 0, TipCD? tipCD = null)
        {
            //Filters results by deadline
            IQueryable<CD> result = _context.CDs;

            if (vitezaDeInscriptionare != 0)
                result = result.Where(f => f.vitezaDeInscriptionare == vitezaDeInscriptionare);

            if (tipCD != null)
                result = result.Where(f => f.tip == tipCD);

            //return await _context.CDs.ToListAsync();
            return await result.ToListAsync();
        }

        /// <summary>
        /// Returneaza un CD specific.
        /// </summary>
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

        /// <summary>
        /// Modifica un CD specific.
        /// </summary>
        // PUT: api/CDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCD(int id, CDUpdateDTO cDUpdateDTO)
        {
            if (!CDExists(id))
                return BadRequest("Id-ul introdus nu exista in baza de date!");

            CD retrievedCD = await _context.CDs.FirstOrDefaultAsync(f => f.id == id);

            if(retrievedCD.tip != TipCD.CDRW 
                && cDUpdateDTO.tipSesiune == TipSesiune.Scriere 
                && retrievedCD.nrDeSesiuni > 0)
                return BadRequest("Tipul acesta de CD nu permite rescrierea datelor!");

            if(cDUpdateDTO.spatiuOcupatAditional + retrievedCD.spatiuOcupat > retrievedCD.dimensiuneMB 
                && cDUpdateDTO.tipSesiune.Equals(TipSesiune.Scriere))
                return BadRequest("Aceasta operatiune ar rezulta in dimensiunea maxima a acestui CD sa fie depasita!");

            decimal spatiuOcupatAditional = (cDUpdateDTO.tipSesiune == TipSesiune.Scriere) ? cDUpdateDTO.spatiuOcupatAditional : 0;
            int nrDeSesiuniAditionale = (cDUpdateDTO.tipSesiune != TipSesiune.Null) ? 1 : 0;
            
            CD tempCD = new CD( cDUpdateDTO.nume,
                                retrievedCD.dimensiuneMB,
                                cDUpdateDTO.vitezaDeInscriptionare,
                                retrievedCD.tip,
                                spatiuOcupatAditional+retrievedCD.spatiuOcupat,
                                retrievedCD.nrDeSesiuni+ nrDeSesiuniAditionale,
                                cDUpdateDTO.tipSesiune
                                );
            tempCD.id = id;

            _context.Entry(retrievedCD).CurrentValues.SetValues(tempCD);

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

            return CreatedAtAction(nameof(GetCD), new { id = tempCD.id }, tempCD);
        }

        /// <summary>
        /// Creaza un CD nou.
        /// </summary>
        // POST: api/CDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CDSetupDTO>> PostCD(CDSetupDTO cDSetupDTO)
        {

            int dimensiuneMB = (cDSetupDTO.tip == TipCD.CDDA) ? 804 : 700;
            if (cDSetupDTO.spatiuOcupat > dimensiuneMB)
                return BadRequest("Spatiul ocupat nu poate depasi capacitatea acestui tip de CD!");

            int nrDeSesiuni = (cDSetupDTO.tipSesiune == TipSesiune.Null) ? 0 : 1;
            
            CD tempCD = new CD(cDSetupDTO.nume, dimensiuneMB, cDSetupDTO.vitezaDeInscriptionare, cDSetupDTO.tip, cDSetupDTO.spatiuOcupat, nrDeSesiuni, cDSetupDTO.tipSesiune);

            _context.CDs.Add(tempCD);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCD", new { id = cD.id }, cD);
            return CreatedAtAction(nameof(GetCD), new { id = tempCD.id }, tempCD);
        }

        /// <summary>
        /// Ordoneaza CD-urile dupa dimensiunea lor in Mb.
        /// </summary>
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

        /// <summary>
        /// Sterge un CD specific.
        /// </summary>
        /// <param name="id"></param> 
        /// <response code="200">Product deleted!</response>
        /// <response code="404">CD not found!</response>
        /// <response code="500">Oops! Can't delete the CD right now</response>
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

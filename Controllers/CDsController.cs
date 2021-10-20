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
        private readonly ICDsService<CD> _cdRepository;

        public CDsController(ICDsService<CD> cdRepository, AppDbContext appDbContext)
        {
            _cdRepository = cdRepository;
        }

        /// <summary>
        /// Returneaza toate CD-urile.
        /// </summary>
        // GET: api/CDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CD>>> GetCDs(bool? orderedByName = false, int? minSpatiuLiber = 0)
        { 
            return Ok(await _cdRepository.GetItemsAsync(orderedByName, minSpatiuLiber));
        }

        /// <summary>
        /// Returneaza datele tuturor CD-urilor bazate pe anumite atribute - viteza de inscriptionare, tipul de CD.
        /// </summary>
        // GET: api/CDs
        [HttpGet("/BasedOnAttributes")]
        public async Task<ActionResult<IEnumerable<CD>>> GetCDsByAttributes(int? vitezaDeInscriptionare = 0, TipCD? tipCD = null)
        {
            return Ok(await _cdRepository.GetItemsAsync(false,0,vitezaDeInscriptionare, tipCD));
        }

        /// <summary>
        /// Returneaza un CD specific.
        /// </summary>
        // GET: api/CDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CD>> GetCD(int id)
        {
            return Ok(await _cdRepository.GetItemByIdAsync(id));
        }

        /// <summary>
        /// Modifica un CD specific.
        /// </summary>
        // PUT: api/CDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCD(int id, CDUpdateDTO cDUpdateDTO)
        {
            return Ok(await _cdRepository.UpdateItemAsync(id, cDUpdateDTO));    
        }

        /// <summary>
        /// Creaza un CD nou.
        /// </summary>
        // POST: api/CDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CD>> PostCD(CDSetupDTO cDSetupDTO)
        {
            return Ok(await _cdRepository.CreateItemAsync(cDSetupDTO));
        }

        /// <summary>
        /// Ordoneaza CD-urile dupa dimensiunea lor in Mb.
        /// </summary>
        // POST: Order api/CDs
        [HttpPost("/OrderBySize")]
        public async Task<ActionResult<IEnumerable<CD>>> OrderBySize(string? orderMethod = "ASC")
        {
           return Ok(await _cdRepository.OrderBySize(orderMethod));
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
            return Ok(await _cdRepository.DeleteItemAsync(id));
        }
    }
}
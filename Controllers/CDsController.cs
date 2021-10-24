using Microsoft.AspNetCore.Mvc;
using GestiuneCD.Models.Specifications;
using GestiuneCD.Services.Interfaces;
using GestiuneCD.Models.Entities;
using GestiuneCD.Models.DTOs;

namespace GestiuneCD.Controllers
{
    public class CDsController : BaseApiController
    {
        private readonly ICDService<CD> _cDsService;

        public CDsController(ICDService<CD> cDsService)
        {
            _cDsService = cDsService;
        }

        /// <summary>
        /// Returneaza toate CD-urile.
        /// </summary>
        // GET: api/CDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CD>>> GetCDs([FromQuery]CDParams parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return Ok(await _cDsService.GetItemsAsync(parameters.orderedByName, parameters.orderedBySize, parameters.minSpatiuLiber,parameters.vitezaMaxInscriptionare,parameters.tipCD,parameters.cuSesiuniDeschise));
        }

        /// <summary>
        /// Returneaza un CD specific.
        /// </summary>
        // GET: api/CDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CD>> GetCD(int id)
        {
            try
            {
                var result = await _cDsService.GetItemByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Modifica un CD specific.
        /// </summary>
        // PUT: api/CDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCD(int id, CDUpdateDTO cDUpdateDTO)
        {
            try
            {
                var result = await _cDsService.UpdateItemAsync(id, cDUpdateDTO);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Creaza un CD nou.
        /// </summary>
        // POST: api/CDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CD>> PostCD(CDSetupDTO cDSetupDTO)
        {
            return Ok(await _cDsService.CreateItemAsync(cDSetupDTO));
        }

        /// <summary>
        /// Sterge un CD specific.
        /// </summary>
        /// <param name="id"></param> 
        /// <response code="204">Product deleted!</response>
        /// <response code="404">CD not found!</response>
        /// <response code="500">Oops! Can't delete the CD right now</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCD(int id)
        {
            try
            {
                var result = await _cDsService.DeleteItemAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
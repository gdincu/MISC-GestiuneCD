using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiuneCD.Models.Entities;
using GestiuneCD.Services.Interfaces;
using System;
using GestiuneCD.Models.DTOs;

namespace GestiuneCD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesiuniController : ControllerBase
    {
        private readonly ISesiuneService<Sesiune> _sesiuneService;

        public SesiuniController(ISesiuneService<Sesiune> sesiuneService)
        {
            _sesiuneService = sesiuneService;
        }

        // GET: api/Sesiuni
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sesiune>>> GetSesiuni()
        {
            return Ok(await _sesiuneService.GetItemsAsync());
        }

        // GET: api/Sesiuni/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sesiune>> GetSesiune(int id)
        {
            try
            {
                var result = await _sesiuneService.GetItemByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // PUT: api/Sesiuni/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSesiune(int id, Sesiune sesiune)
        {
            try
            {
                var result = await _sesiuneService.UpdateItemAsync(id, sesiune);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/Sesiuni
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sesiune>> PostSesiune(SesiuneSetupDTO sesiune)
        {
            try
            {
                var result = await _sesiuneService.CreateItemAsync(sesiune);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // DELETE: api/Sesiuni/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSesiune(int id)
        {
            try
            {
                var result = await _sesiuneService.DeleteItemAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RP_Ovning15.Core.Entities;
using RP_Ovning15.Data.Data;

namespace RP_Ovning15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext db;

        public ModulesController(LmsApiContext context)
        {
            db = context;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
          if (db.Module == null)
          {
              return NotFound();
          }
            return await db.Module.ToListAsync();
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
          if (db.Module == null)
          {
              return NotFound();
          }
            var @module = await db.Module.FindAsync(id);

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            db.Entry(@module).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
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

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
          if (db.Module == null)
          {
              return Problem("Entity set 'LmsApiContext.Module'  is null.");
          }
            db.Module.Add(@module);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            if (db.Module == null)
            {
                return NotFound();
            }
            var @module = await db.Module.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            db.Module.Remove(@module);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return (db.Module?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

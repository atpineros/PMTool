using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;

namespace PMTool.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequirementApiController : ControllerBase
    {
        private readonly prj6633t2Context _context;

        public RequirementApiController(prj6633t2Context context)
        {
            _context = context;
        }

        // GET: api/Requirements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requirements>>> GetRequirements()
        {
            return await _context.Requirements.ToListAsync();
        }

        // GET: api/Requirements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requirements>> GetRequirements(int id)
        {
            var requirements = await _context.Requirements.FindAsync(id);

            if (requirements == null)
            {
                return NotFound();
            }

            return requirements;
        }

        [HttpGet("projId/{projId}")]
        public ActionResult<IEnumerable<Requirements>> GetRequirementsByProjId(int projId)
        {
            var requirements = _context.Requirements.Where(x => x.PrjIdFk == projId).ToList();

            if (requirements == null)
            {
                return NotFound();
            }

            return requirements;
        }

        // PUT: api/Requirements/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequirements(int id, Requirements requirements)
        {
            if (id != requirements.ReqId)
            {
                return BadRequest();
            }

            _context.Entry(requirements).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementsExists(id))
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

        // POST: api/Requirements
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Projects>> PostRequirements(Requirements requirements)
        {
            _context.Requirements.Add(requirements);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjects", new { id = requirements.ReqId }, requirements);
        }

        // DELETE: api/Requirements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Requirements>> DeleteRequirements(int id)
        {
            var requirements = await _context.Requirements.FindAsync(id);
            if (requirements == null)
            {
                return NotFound();
            }

            _context.Requirements.Remove(requirements);
            await _context.SaveChangesAsync();

            return requirements;
        }

        private bool RequirementsExists(int id)
        {
            return _context.Requirements.Any(e => e.ReqId == id);
        }
    }
}

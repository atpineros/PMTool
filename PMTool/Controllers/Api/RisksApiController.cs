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
    public class RisksApiController : ControllerBase
    {
        private readonly prj6633t2Context _context;

        public RisksApiController(prj6633t2Context context)
        {
            _context = context;
        }

        // GET: api/Risks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Risks>>> GetRisks()
        {
            return await _context.Risks.ToListAsync();
        }


        [HttpGet("risks/{id}")]
        public async Task<ActionResult<IEnumerable<Risks>>> GetRisksbyProjectID(int id)
        {
            return await _context.Risks.Where(x => x.ProjIdFk == id).ToListAsync();
        }


        //GET: api/Risks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Risks>> GetRisks(int id)
        {
            var risks = await _context.Risks.FindAsync(id);

            if (risks == null)
            {
                return NotFound();
            }

            return risks;
        }

        // PUT: api/Risks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRisks(int id, Risks risks)
        //{
        //    if (id != risks.RiskId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(risks).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RisksExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Risks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Risks>> PostRisks(Risks risks)
        {
            _context.Risks.Add(risks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRisks", new { id = risks.RiskId }, risks);
        }

        // DELETE: api/Risks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Risks>> DeleteRisks(int id)
        {
            var risks = await _context.Risks.FindAsync(id);
            if (risks == null)
            {
                return NotFound();
            }

            _context.Risks.Remove(risks);
            await _context.SaveChangesAsync();

            return risks;
        }

    }
}

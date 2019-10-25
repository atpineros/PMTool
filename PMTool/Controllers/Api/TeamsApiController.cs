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
    public class TeamsApiController : ControllerBase
    {
        private readonly prj6633t2Context _context;

        public TeamsApiController(prj6633t2Context context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teams>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Teams>>> GetTeams(int id)
        {
            var teams = await _context.Teams.Where(x=>x.PriIdFk ==id).ToListAsync();

            if (teams == null)
            {
                return NotFound();
            }

            return teams;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeams(int id, Teams teams)
        {
            if (id != teams.TeamId)
            {
                return BadRequest();
            }

            _context.Entry(teams).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamsExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Teams>> PostTeams(Teams teams)
        {
            _context.Teams.Add(teams);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeams", new { id = teams.TeamId }, teams);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teams>> DeleteTeams(int id)
        {
            var teams = await _context.Teams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(teams);
            await _context.SaveChangesAsync();

            return teams;
        }

        private bool TeamsExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using PMTool.Models.ViewModels;

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

        
        [HttpGet("GetTeamByProjectID/{id}")]
        public async Task<ProjectViewViewModel> GetTeamByProjectID(int id)        
        {
            ProjectViewViewModel projectViewViewModel = new ProjectViewViewModel();
            var listOfUsers = new List<User>();
            var listOfRoles = new List<Role>();
            var UsersByProjectID = from t in _context.Teams
                                         join u in _context.User on t.UserIdFk equals u.UserId
                                         join r in _context.Role on t.RoleIdFk equals r.RoleId
                                         where t.PriIdFk == id
                                         select new { u.Fname, u.Lname, r.RoleName };
            var ListOfUsersByProjectID = await UsersByProjectID.ToListAsync();
            foreach ( var item in ListOfUsersByProjectID)
            {
                listOfUsers.Add(new User { Fname = item.Fname, Lname = item.Lname});
                listOfRoles.Add( new Role { RoleName = item.RoleName });
            }
            projectViewViewModel.Roles = listOfRoles;
            projectViewViewModel.Users = listOfUsers;
            
            return projectViewViewModel;

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

using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using League.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace League.Pages.Teams
{
    public class TeamModel : PageModel
    {
        //injetct EF context
        private readonly LeagueContext context;

        public TeamModel(LeagueContext context)
        {
            this.context = context;
        }

        //load a single team based on id and related division players
        public Team Team { get; set; }

        public async Task OnGetAsync(string Id)
        {
            Team = await this.context.Teams
                .Include(t => t.Players)
                .Include(t => t.Division)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TeamId == Id);
        }
    }
}

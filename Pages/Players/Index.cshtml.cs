using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using League.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext context;

        public IndexModel(LeagueContext context)
        {
            this.context = context;
        }

        public List<Player> Players { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }  
        
        public SelectList Teams { get; set; }
        public SelectList Positions { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedTeam { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedPosition { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; } = "Name";

        public string FavoriteTeam { get; set; }

        public async Task OnGetAsync()
        {
            var players = from p in context.Players 
                          select p;
            if (!string.IsNullOrEmpty(SearchString))
            {
                players = players.Where(p => p.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(SelectedTeam))
            {
                players = players.Where(p => p.TeamId == SelectedTeam);
            }

            if (!string.IsNullOrEmpty(SelectedPosition))
            {
                players = players.Where(p => p.Position == SelectedPosition);
            }

            switch (SortField)
            {
                case "Number": players = players.OrderBy(p => p.Number).ThenBy(p => p.TeamId); break;
                case "Name": players = players.OrderBy(p => p.Name).ThenBy(p => p.TeamId); break;
                case "Position": players = players.OrderBy(p => p.Position).ThenBy(p => p.TeamId); break;
            }

            IQueryable<string> teamQuery = from p in context.Teams
                                           orderby p.TeamId
                                           select p.TeamId;
            Teams = new SelectList(await teamQuery.ToListAsync());

            IQueryable<string> positionQuery = from p in context.Players
                                               orderby p.Position
                                               select p.Position;
            Positions = new SelectList(await positionQuery.Distinct().ToListAsync());

            FavoriteTeam = HttpContext.Session.GetString("_Favorite");

            Players = await players.ToListAsync();

            
        }
        public string PlayerClass(Player Player)
        {
            string Class = "d-flex";
            if (Player.Depth == 1)
                Class += " starter";
            if (Player.TeamId == FavoriteTeam)
                Class += " favorite";
            return Class;
        }

    }
}

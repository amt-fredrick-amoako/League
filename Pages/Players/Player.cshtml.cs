using League.Data;
using League.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace League.Pages.Players
{
    public class PlayerModel : PageModel
    {
        private readonly LeagueContext context;

        public PlayerModel(LeagueContext context)
        {
            this.context = context;
        }

        public Player Player { get; set; }

        public async Task OnGetAsync(string id)
        {
            Player = await context.Players.FindAsync(id);
        }
    }
}

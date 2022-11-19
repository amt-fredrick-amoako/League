using League.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace League.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly LeagueContext context;

        public IndexModel(ILogger<IndexModel> logger, LeagueContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public League.Models.League League { get; set; }

        public async Task OnGetAsync()
        {
            League = await this.context.Leagues.FirstOrDefaultAsync();
        }
    }
}

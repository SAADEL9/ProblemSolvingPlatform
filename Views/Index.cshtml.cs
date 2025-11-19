using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProblemSolvingPlatform.Models;

namespace ProblemSolvingPlatform.Views
{
    public class IndexModel : PageModel
    {
        private readonly ProblemSolvingPlatform.Models.ProblemSolvingPlatformContext _context;

        public IndexModel(ProblemSolvingPlatform.Models.ProblemSolvingPlatformContext context)
        {
            _context = context;
        }

        public IList<Probleme> Probleme { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Probleme = await _context.Problemes.ToListAsync();
        }
    }
}

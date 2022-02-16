#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AndrecutEmanuelMedii.Data;
using AndrecutEmanuelMedii.Models;

namespace AndrecutEmanuelMedii.Pages.Wagons
{
    public class IndexModel : PageModel
    {
        private readonly AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext _context;

        public IndexModel(AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext context)
        {
            _context = context;
        }

        public IList<Wagon> Wagon { get;set; }

        public async Task OnGetAsync()
        {
            Wagon = await _context.Wagon
                .Include(w => w.Train).ToListAsync();
        }
    }
}

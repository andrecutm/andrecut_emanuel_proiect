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

namespace AndrecutEmanuelMedii.Pages.Trains
{
    public class DetailsModel : PageModel
    {
        private readonly AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext _context;

        public DetailsModel(AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext context)
        {
            _context = context;
        }

        public Train Train { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Train = await _context.Train
                .Include(t => t.TrainStation)
                .Include(t => t.DestinationTrainStation)
                .Include(t => t.Wagons).ThenInclude(w => w.Facilities)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Train == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

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

namespace AndrecutEmanuelMedii.Pages.TrainStations
{
    public class DeleteModel : PageModel
    {
        private readonly AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext _context;

        public DeleteModel(AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TrainStation TrainStation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrainStation = await _context.TrainStation.FirstOrDefaultAsync(m => m.ID == id);

            if (TrainStation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrainStation = await _context.TrainStation.FindAsync(id);

            if (TrainStation != null)
            {
                _context.TrainStation.Remove(TrainStation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

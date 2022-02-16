#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AndrecutEmanuelMedii.Data;
using AndrecutEmanuelMedii.Models;

namespace AndrecutEmanuelMedii.Pages.Trains
{
    public class EditModel : PageModel
    {
        private readonly AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext _context;

        public EditModel(AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext context)
        {
            _context = context;
        }

        [BindProperty]
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
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Train == null)
            {
                return NotFound();
            }
            var trainsList = new SelectList(_context.TrainStation, "ID", "Name");
            ViewData["TrainStationID"] = trainsList;
            ViewData["DestinationTrainStationID"] = trainsList;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // validate free seats
            if (Train.TrainStationID.Equals(Train.DestinationTrainStationID))
            {
                ViewData["DestinationTrainStationError"] = "Destination station should be different!";
                await this.OnGetAsync(Train.ID); // so we can keep data
                return Page();
            }

            _context.Attach(Train).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(Train.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TrainExists(int id)
        {
            return _context.Train.Any(e => e.ID == id);
        }
    }
}

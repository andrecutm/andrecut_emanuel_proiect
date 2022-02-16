#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AndrecutEmanuelMedii.Data;
using AndrecutEmanuelMedii.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AndrecutEmanuelMedii.Pages.Trains
{
    public class CreateModel : PageModel
    {
        private readonly AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext _context;

        public CreateModel(AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var trainsList = new SelectList(_context.TrainStation, "ID", "Name");
            ViewData["TrainStationID"] = trainsList;
            ViewData["DestinationTrainStationID"] = trainsList;
            return Page();
        }

        [BindProperty]
        public Train Train { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Page();
            }

            // validate free seats
            if (Train.TrainStationID.Equals(Train.DestinationTrainStationID))
            {
                ViewData["DestinationTrainStationError"] = "Destination station must be different!!";
                this.OnGet(); // so we can keep data
                return Page();
            }

            _context.Train.Add(Train);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

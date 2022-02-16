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

namespace AndrecutEmanuelMedii.Pages.Wagons
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
            ViewData["TrainId"] = new SelectList(_context.Train, "ID", "ID");
            FormFacilitiesIds = new int[_context.Facility.Count()];
            ViewData["Facilities"] = new MultiSelectList(_context.Facility, "ID", "Name", FormFacilitiesIds);
            return Page();
        }

        [BindProperty]
        public Wagon Wagon { get; set; }
        [BindProperty]
        public int[] FormFacilitiesIds { get; set; } // form facilities

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Page();
            }

            // validate free seats
            if (Wagon.FreeSeats > Wagon.TotalSeats)
            {
                ViewData["FreeSeatsError"] = "It's not possible to have more free seats than total seats!";
                this.OnGet(); // so we can keep data
                return Page();
            }

            _context.Wagon.Add(Wagon);

            Wagon.Facilities = new List<Facility>();
            // set the facilities ids to the new set
            foreach (var id in FormFacilitiesIds)
            {
                var facility = _context.Facility.First(s => s.ID == id);
                Wagon.Facilities.Add(facility);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

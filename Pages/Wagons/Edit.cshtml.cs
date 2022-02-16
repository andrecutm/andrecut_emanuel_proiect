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

namespace AndrecutEmanuelMedii.Pages.Wagons
{
    public class EditModel : PageModel
    {
        private readonly AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext _context;

        public EditModel(AndrecutEmanuelMedii.Data.AndrecutEmanuelMediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Wagon Wagon { get; set; }

        [BindProperty]
        public int[] GetExistingSelectedFacilitiesIds { get; set; } // form facilities

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Wagon = await _context.Wagon
                .Include(w => w.Train)
                .Include(w => w.Facilities)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Wagon == null)
            {
                return NotFound();
            }

            //
            int itemCount = 0;
            GetExistingSelectedFacilitiesIds = new int [Wagon.Facilities.Count]; // on get, initialize with already existing facilities
            foreach (var item in Wagon.Facilities)
            {
                GetExistingSelectedFacilitiesIds[itemCount] = item.ID;
                itemCount++;
            }

            ViewData["TrainId"] = new SelectList(_context.Train, "ID", "ID");
            ViewData["Facilities"] = new MultiSelectList(_context.Facility, "ID", "Name", GetExistingSelectedFacilitiesIds);
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
            if (Wagon.FreeSeats > Wagon.TotalSeats)
            {
                ViewData["FreeSeatsError"] = "It's not possible to have more free seats than total seats!";
                await this.OnGetAsync(Wagon.ID); // so we can keep data
                return Page();
            }

            _context.Attach(Wagon).State = EntityState.Modified;

            try
            {
         
                // init the new set of facilities
                if (Wagon.Facilities == null)  Wagon.Facilities = new List<Facility>();

                // clear all old facilities before saving the new set
                var facilities = _context.Facility
                    .Where(f => f.Wagons.Any(w => w.ID == Wagon.ID)).Include(f => f.Wagons);
                foreach (var facility in facilities)
                {
                    facility.Wagons.Remove(Wagon);
                }

                // set the facilities ids to the new set
                foreach (var id in GetExistingSelectedFacilitiesIds)
                {
                    var facility = _context.Facility.First(s => s.ID == id);
                    Wagon.Facilities.Add(facility);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WagonExists(Wagon.ID))
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

        private bool WagonExists(int id)
        {
            return _context.Wagon.Any(e => e.ID == id);
        }
    }
}

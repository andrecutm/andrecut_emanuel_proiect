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

namespace AndrecutEmanuelMedii.Pages.Facilities
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
            return Page();
        }

        [BindProperty]
        public Facility Facility { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Facility.Add(Facility);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

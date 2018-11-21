using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.Programs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public EditModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Programs Programs { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Programs = await _context.Programs.FirstOrDefaultAsync(m => m.ProgramsID == id);

            if (Programs == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Programs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramsExists(Programs.ProgramsID))
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

        private bool ProgramsExists(string id)
        {
            return _context.Programs.Any(e => e.ProgramsID == id);
        }
    }
}

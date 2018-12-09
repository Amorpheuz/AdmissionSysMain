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

namespace AdmissionSys.Pages.ApplicationList
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
        public Models.ApplicationList ApplicationList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationList = await _context.ApplicationList
                .Include(a => a.Programs).FirstOrDefaultAsync(m => m.ApplicationListID == id);

            if (ApplicationList == null)
            {
                return NotFound();
            }
           ViewData["ProgramsID"] = new SelectList(_context.Programs, "ProgramsID", "ProgramsID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ApplicationList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationListExists(ApplicationList.ApplicationListID))
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

        private bool ApplicationListExists(int id)
        {
            return _context.ApplicationList.Any(e => e.ApplicationListID == id);
        }
    }
}

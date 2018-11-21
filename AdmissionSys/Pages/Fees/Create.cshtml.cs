using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.Fees
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public CreateModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProgramsID"] = new SelectList(_context.Programs, "ProgramsID", "ProgramsID");
            return Page();
        }

        [BindProperty]
        public Models.Fees Fees { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Fees.Add(Fees);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
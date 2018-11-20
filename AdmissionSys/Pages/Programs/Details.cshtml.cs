using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;

namespace AdmissionSys.Pages.Programs
{
    public class DetailsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public DetailsModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;

namespace AdmissionSys.Pages.Fees
{
    public class DetailsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public DetailsModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public Models.Fees Fees { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fees = await _context.Fees
                .Include(f => f.Programs).FirstOrDefaultAsync(m => m.FeesID == id);

            if (Fees == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

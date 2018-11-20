using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;

namespace AdmissionSys.Pages.ApplicationList
{
    public class DetailsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public DetailsModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}

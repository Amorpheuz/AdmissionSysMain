using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.Documents
{
    [Authorize(Roles = "Applicant,Admin,Approver")]
    public class DetailsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public DetailsModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public Models.Documents Documents { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Documents = await _context.Documents.FirstOrDefaultAsync(m => m.DocumentsID == id);

            if (Documents == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

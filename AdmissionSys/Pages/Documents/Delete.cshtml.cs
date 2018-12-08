using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace AdmissionSys.Pages.Documents
{
    [Authorize(Roles = "Applicant,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public DeleteModel(AdmissionSys.Models.AdmissionSysContext context, IHostingEnvironment environment)
        {
            _context = context;
            this.hostingEnvironment = environment;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Documents = await _context.Documents.FindAsync(id);

            if(System.IO.File.Exists(hostingEnvironment.WebRootPath+Documents.DocumentPath+""))
            {
                System.IO.File.Delete(hostingEnvironment.WebRootPath + Documents.DocumentPath + "");
            }
            if (Documents != null)
            {
                _context.Documents.Remove(Documents);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.Student
{
    [Authorize(Roles = "Admin,Applicant,Approver")]
    public class DetailsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public DetailsModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public Models.Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.StudentID == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Pages.AcademicRecord.Exams
{
    [Authorize(Roles = "Applicant,Admin")]
    public class sscModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public sscModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.AcademicRecord AcademicRecord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
           AcademicRecord.MarksObtained=Request.Form["marksobtained"].ToString().Replace(",", "");
            AcademicRecord.ObtainedOutOfOrCGPA = Request.Form["outofobtained"].ToString().Replace(",", "");
            AcademicRecord.CalcPer = Request.Form["CalcPer"].ToString().Replace(",","");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AcademicRecord.Add(AcademicRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}

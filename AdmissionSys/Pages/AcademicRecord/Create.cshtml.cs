using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.AcademicRecord
{
    [Authorize(Roles = "Applicant,Admin")]
    public class CreateModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public CreateModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string ex)
        {
            if(ex=="ssc")
            {
                return RedirectToPage("./Exams/ssc/ssc");
            }
            if (ex == "hsc")
            {
                return RedirectToPage("./Exams/hsc/hsc");
            }
            if (ex == "certi")
            {
                return RedirectToPage("./Exams/certi/certi");
            }
            if (ex == "diploma")
            {
                return RedirectToPage("./Exams/diploma/diploma");
            }
            if (ex == "graduate")
            {
                return RedirectToPage("./Exams/graduate/graduate");
            }
            return Page();
        }

        [BindProperty]
        public Models.AcademicRecord AcademicRecord { get; set; }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AcademicRecord.Add(AcademicRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
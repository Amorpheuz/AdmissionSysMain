using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AdmissionSys.Areas.Identity.Data;

namespace AdmissionSys.Pages.AcademicRecord
{
    [Authorize(Roles = "Applicant,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public DeleteModel(AdmissionSys.Models.AdmissionSysContext context ,UserManager<NuvAdUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Models.AcademicRecord AcademicRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

            AcademicRecord = await _context.AcademicRecord.FirstOrDefaultAsync(m => m.AcademicRecordID == id && m.StudentID==sturecord.FirstOrDefault().StudentID);

            if (AcademicRecord == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcademicRecord = await _context.AcademicRecord.FindAsync(id);

            if (AcademicRecord != null)
            {
                _context.AcademicRecord.Remove(AcademicRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

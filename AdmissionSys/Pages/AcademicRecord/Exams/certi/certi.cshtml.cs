using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Pages.AcademicRecord.Exams.certi
{
    public class certiModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public certiModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));
            //IQueryable<AcademicRecord> academicRecordsIQ = (System.Linq.IQueryable<AdmissionSys.Pages.AcademicRecord>)(from s in _context.AcademicRecord select s);
            var academicRecordsIQ = from a in _context.AcademicRecord select a;
            academicRecordsIQ = academicRecordsIQ.Where(a => a.ExamName.Contains("Certificate") && a.StudentID == sturecord.FirstOrDefault().StudentID);

            if (academicRecordsIQ.Count() >= 1)
            {
                return RedirectToPage("../../Index");
            }
            else
            {
                return Page();
            }

        }

        [BindProperty]
        public Models.AcademicRecord AcademicRecord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));
            AcademicRecord.StudentID = sturecord.FirstOrDefault().StudentID;

            AcademicRecord.MarksObtained = Request.Form["marksobtained"].ToString().Replace(",", "");
            AcademicRecord.ObtainedOutOfOrCGPA = Request.Form["outofobtained"].ToString().Replace(",", "");
            AcademicRecord.CalcPer = Request.Form["CalcPer"].ToString().Replace(",", "");
            AcademicRecord.CalcPer = AcademicRecord.CalcPer.Replace("%", "");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AcademicRecord.Add(AcademicRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("../../Index");
        }
    }
}

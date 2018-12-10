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

namespace AdmissionSys.Pages.Admin.Student
{
    [Authorize(Roles = "Admin,Approver")]
    public class DetailsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public DetailsModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Models.Student> Student { get; set; }
        public IList<Models.AcademicRecord> AcademicRecord { get; set; }

        public IList<Models.AcademicRecord> AcademicRecordHSC { get; set; }

        public IList<Models.AcademicRecord> AcademicRecordCerti { get; set; }

        public IList<Models.AcademicRecord> AcademicRecordDiploma { get; set; }

        public IList<Models.AcademicRecord> AcademicRecordGraduate { get; set; }

        public IList<Models.ApplicationList> ApplicationList { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NuvAdUser user = await GetCurrentUserAsync();
            var stuAllDets = from s in _context.Student select s;
            stuAllDets = stuAllDets.Where(ab => ab.StudentID.Equals(id));
            Student = stuAllDets.ToList();
            if (Student == null)
            {
                return NotFound();
            }
            var academicRecordsIQ = from a in _context.AcademicRecord select a;
            AcademicRecord = academicRecordsIQ.Where(a => a.StudentID == stuAllDets.FirstOrDefault().StudentID && a.ExamName.Equals("SSC/Std 10th")).ToList();
            AcademicRecordHSC = academicRecordsIQ.Where(a => a.StudentID == stuAllDets.FirstOrDefault().StudentID && a.ExamName.Equals("HSC/Std 12th")).ToList();
            AcademicRecordCerti = academicRecordsIQ.Where(a => a.StudentID == stuAllDets.FirstOrDefault().StudentID && a.ExamName.Equals("Certificate")).ToList();
            AcademicRecordDiploma = academicRecordsIQ.Where(a => a.StudentID == stuAllDets.FirstOrDefault().StudentID && a.ExamName.Equals("Diploma")).ToList();
            AcademicRecordGraduate = academicRecordsIQ.Where(a => a.StudentID == stuAllDets.FirstOrDefault().StudentID && a.ExamName.Equals("Graduate/Post Graduate")).ToList();

            ApplicationList = await _context.ApplicationList.Include(a => a.Programs).Where(c => c.StudentID == stuAllDets.FirstOrDefault().StudentID).ToListAsync();

            return Page();
        }
    }
}
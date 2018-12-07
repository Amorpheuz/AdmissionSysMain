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
    public class IndexModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IndexModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Models.AcademicRecord> AcademicRecord { get;set; }

        public IList<Models.AcademicRecord> AcademicRecordHSC { get; set; }

        public IList<Models.AcademicRecord> AcademicRecordCerti { get; set; }

        public IList<Models.AcademicRecord> AcademicRecordDiploma { get; set; }

        public IList<Models.AcademicRecord> AcademicRecordGraduate { get; set; }

        public async Task OnGetAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

            var academicRecordsIQ = from a in _context.AcademicRecord select a;
            AcademicRecord = academicRecordsIQ.Where(a => a.StudentID == sturecord.FirstOrDefault().StudentID && a.ExamName.Equals("SSC/Std 10th")).ToList();
            AcademicRecordHSC = academicRecordsIQ.Where(a => a.StudentID == sturecord.FirstOrDefault().StudentID && a.ExamName.Equals("HSC/Std 12th")).ToList();
            AcademicRecordCerti = academicRecordsIQ.Where(a => a.StudentID == sturecord.FirstOrDefault().StudentID && a.ExamName.Equals("Certificate")).ToList();
            AcademicRecordDiploma = academicRecordsIQ.Where(a => a.StudentID == sturecord.FirstOrDefault().StudentID && a.ExamName.Equals("Diploma")).ToList();
            AcademicRecordGraduate= academicRecordsIQ.Where(a => a.StudentID == sturecord.FirstOrDefault().StudentID && a.ExamName.Equals("Graduate/Post Graduate")).ToList();

        }
    }
}

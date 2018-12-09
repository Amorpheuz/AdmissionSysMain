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

namespace AdmissionSys.Pages.Student
{
    public class PrintFormModel : PageModel
    {

        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public PrintFormModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
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
        public bool render;

        public async Task OnGetAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var stuAllDets = from s in _context.Student select s;
            Student = stuAllDets.Where(ab => ab.userID.Equals(user.Id)).ToList();
            
        }
    }
}
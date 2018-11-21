using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.AcademicRecord
{
    [Authorize(Roles = "Applicant,Admin")]
    public class IndexModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public IndexModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public IList<Models.AcademicRecord> AcademicRecord { get;set; }

        public async Task OnGetAsync()
        {
            AcademicRecord = await _context.AcademicRecord.ToListAsync();
        }
    }
}

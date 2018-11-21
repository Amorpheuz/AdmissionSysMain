using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.ApplicationList
{
    [Authorize(Roles = "Applicant,Admin")]
    public class IndexModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public IndexModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public IList<Models.ApplicationList> ApplicationList { get;set; }

        public async Task OnGetAsync()
        {
            ApplicationList = await _context.ApplicationList
                .Include(a => a.Programs).ToListAsync();
        }
    }
}

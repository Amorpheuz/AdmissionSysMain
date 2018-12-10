using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdmissionSys.Pages.Admin.Student
{
    [Authorize(Roles = "Admin,Approver")]
    public class allapplicationsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;

        public allapplicationsModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _userManager = userManager;
            _context = context;

        }

        public IList<Models.ApplicationList> ApplicationList { get; set; }

        public async Task OnGetAsync(int? id)
        {
            ApplicationList = await _context.ApplicationList
                .Include(a => a.Programs).Where(c => c.StudentID == id).ToListAsync();

        }

    }
}

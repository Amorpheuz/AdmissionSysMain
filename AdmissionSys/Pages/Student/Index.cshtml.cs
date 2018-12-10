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
    [Authorize(Roles = "Admin,Applicant,Approver")]
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

        public IList<Models.Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            // Student = await _context.Student.ToListAsync();
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            Student = sturecord.Where(ab => ab.userID.Equals(user.Id)).ToList();
        }
    }
}

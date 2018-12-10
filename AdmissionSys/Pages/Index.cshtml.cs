using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Pages
{
    [Authorize(Roles = "Admin,Applicant,Approver")]
    public class IndexModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public IndexModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _userManager = userManager;
            _context = context;
          
        }
        public IList<Models.Student> Students { get; set; }
        public IList<Models.ApplicationList> ApplicationLists { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            // var sturec = _context.Student.Where(c => c.StudentID == id);
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

            Students = sturecord.ToList();
            var applicationrec = from bb in _context.ApplicationList select bb;
            applicationrec = applicationrec.Where(a => a.StudentID == sturecord.FirstOrDefault().StudentID);
            ApplicationLists = applicationrec.ToList();
            return Page();
        }
    }
}

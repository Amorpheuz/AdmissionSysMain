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

namespace AdmissionSys.Pages.ApplicationList
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

        public IList<Models.ApplicationList> ApplicationList { get;set; }

        public async Task OnGetAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

          //  var applrec = from p in _context.ApplicationList select p;
            //applrec = applrec.Where(c => c.StudentID == sturecord.FirstOrDefault().StudentID);

            //var prgrec = from p in _context.Programs select p;
            //prgrec = prgrec.Where(ab => ab.ProgramsID == applrec.FirstOrDefault().ProgramsID);


            ApplicationList = await _context.ApplicationList
                .Include(a => a.Programs).Where(c => c.StudentID == sturecord.FirstOrDefault().StudentID).ToListAsync();
        }
    }
}

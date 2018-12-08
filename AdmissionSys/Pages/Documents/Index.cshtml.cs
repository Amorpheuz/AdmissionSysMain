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

namespace AdmissionSys.Pages.Documents
{
    [Authorize(Roles = "Applicant,Admin")]
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

        public IList<Models.Documents> Documents { get;set; }

        public async Task OnGetAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

            var DocumentsIQ = from a in _context.Documents select a;

            Documents = DocumentsIQ.Where(b => b.StudentID == sturecord.FirstOrDefault().StudentID).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Pages.Admin.Document
{
    [Authorize(Roles = "Admin")]
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

        public IList<Models.Documents> Documents { get; set; }

        public async Task OnGetAsync(int? id)
        {
            var DocumentsIQ = from a in _context.Documents select a;

            Documents = DocumentsIQ.Where(b => b.StudentID == id).ToList();
        }

    }
}

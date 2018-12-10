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

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.Student> Students { get; set; }


        //public IList<Models.Student> Student { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Models.Student> studentsIQ = from s in _context.Student select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.StudentName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.StudentName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.LastOpr);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastOpr);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastOpr);
                    break;
            }

            int pageSize = 3;
            Students = await PaginatedList<Models.Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}

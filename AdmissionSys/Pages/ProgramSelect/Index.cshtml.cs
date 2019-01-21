using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdmissionSys.Pages.ProgramSelect
{
    [Authorize(Roles = "Admin,Applicant,Approver")]
    public class IndexModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public IndexModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.Programs> Programs { get; set; }

        public async Task OnGetAsync(string sortOrder,string currentFilter, string searchString, int? pageIndex)
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

            IQueryable<Models.Programs> programsIQ = from s in _context.Programs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                programsIQ = programsIQ.Where(s => s.ProgramName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    programsIQ = programsIQ.OrderByDescending(s => s.ProgramName);
                    break;
                case "Date":
                    programsIQ = programsIQ.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    programsIQ = programsIQ.OrderByDescending(s => s.StartDate);
                    break;
                case "end_date_desc":
                    programsIQ = programsIQ.OrderByDescending(s => s.EndDate);
                    break;
                case "end_Date":
                    programsIQ = programsIQ.OrderBy(s => s.EndDate);
                    break;
                default:
                    programsIQ = programsIQ.OrderBy(s => s.IsIntakeOpen);
                    break;
            }

            int pageSize = 3;
            Programs = await PaginatedList<Models.Programs>.CreateAsync(
                programsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}

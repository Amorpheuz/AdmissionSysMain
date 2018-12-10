using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdmissionSys.Pages.Admin.Student
{
    public class moreoptionsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;

        public moreoptionsModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        
        }

        [BindProperty]
        public Models.ApplicationList ApplicationList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sturec = _context.Student.Where(c => c.StudentID == id);

            ApplicationList =  _context.ApplicationList.FirstOrDefault(m => m.ApplicationListID==id);

            if (ApplicationList == null)
            {
                return NotFound();
            }
          
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ApplicationList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationListExists(ApplicationList.ApplicationListID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ApplicationListExists(int id)
        {
            return _context.ApplicationList.Any(e => e.ApplicationListID == id);
        }

    }
}

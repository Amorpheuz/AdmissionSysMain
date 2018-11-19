using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Identity;

namespace AdmissionSys.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public EditModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Models.Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.StudentID == id);

            if (Student == null)
            {
                return NotFound();
            }
            NuvAdUser user = await GetCurrentUserAsync();
            ViewData["userId"] = user?.Id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.StudentID))
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

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
    }
}

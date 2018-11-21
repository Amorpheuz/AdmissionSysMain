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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AdmissionSys.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public EditModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager,IHostingEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            this.hostingEnvironment = environment;
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
            await savephotoAsync();
            await savesignAsync();
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


        private async Task<IActionResult> savephotoAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            string path = hostingEnvironment.WebRootPath + "//uploads//" + user?.Id;
            if (Student.StudentPhotoActual != null)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = GetUniqueName(Student.StudentPhotoActual.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, path);
                var filePath = Path.Combine(uploads, fileName);
                Student.StudentPhotoActual.CopyTo(new FileStream(filePath, FileMode.Create));
                Student.StudentPhoto = filePath; // Set the file name
                return null;
            }
            else
            {
                return Page();
            }
        }
        private async Task<IActionResult> savesignAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            string path = hostingEnvironment.WebRootPath + "//uploads//" + user?.Id;
            if (Student.StudentSignatureActual != null)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = GetUniqueName(Student.StudentSignatureActual.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, path);
                var filePath = Path.Combine(uploads, fileName);
                Student.StudentSignatureActual.CopyTo(new FileStream(filePath, FileMode.Create));
                Student.StudentSignature = filePath; // Set the file name
                return null;
            }
            else
            {
                return Page();
            }
        }

        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}

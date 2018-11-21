using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.Student
{
    [Authorize(Roles = "Admin,Applicant")]
    public class CreateModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CreateModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            this.hostingEnvironment = environment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            ViewData["userId"] = user?.Id;
            return Page();
        }

        [BindProperty]
        public Models.Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //Student.StudentSignature = "wwrc";
            await savephotoAsync();
            await savesignAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Student.Add(Student);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
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
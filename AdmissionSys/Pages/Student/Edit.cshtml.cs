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
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSys.Pages.Student
{
    [Authorize(Roles = "Admin,Applicant,Approver")]
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
            //await SavephotoAsync();
            //await SavesignAsync();
          
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


        //private async Task<IActionResult> SavephotoAsync()
        //{
        //    NuvAdUser user = await GetCurrentUserAsync();
        //    string path = hostingEnvironment.WebRootPath + "/uploads/" + Student.StudentID;
        //    if (Student.StudentPhotoActual != null)
        //    {
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        string filepathimg = Student.StudentPhoto;
        //        string filepathsign = Student.StudentSignature;

        //        if (System.IO.File.Exists(hostingEnvironment.WebRootPath+"/"+filepathimg))
        //        {
        //            System.IO.File.Delete(hostingEnvironment.WebRootPath+""+filepathimg);
        //        }
        //        if (System.IO.File.Exists(hostingEnvironment.WebRootPath+"/"+filepathsign))
        //        {
        //            System.IO.File.Delete(hostingEnvironment.WebRootPath + "/" + filepathsign);
        //        }
        //        var fileName = GetUniqueName(Student.StudentPhotoActual.FileName);
        //        var uploads = Path.Combine(hostingEnvironment.WebRootPath, path);
        //        var filePath = Path.Combine(uploads, fileName);
        //        Student.StudentPhotoActual.CopyTo(new FileStream(filePath, FileMode.Create));
        //        Student.StudentPhoto = "/uploads/" + Student.StudentID +"/"+ fileName; // Set the file name
        //        return null;
        //    }
        //    else
        //    {
        //        return Page();
        //    }
        //}
        //private async Task<IActionResult> SavesignAsync()
        //{
        //    NuvAdUser user = await GetCurrentUserAsync();
        //    string path = hostingEnvironment.WebRootPath + "/uploads/" + Student.StudentID;
        //    if (Student.StudentSignatureActual != null)
        //    {
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        string filepathimg = Student.StudentPhoto;
        //        string filepathsign = Student.StudentSignature;

        //        if (System.IO.File.Exists(filepathimg))
        //        {
        //            System.IO.File.Delete(filepathimg);
        //        }
        //        if (System.IO.File.Exists(filepathsign))
        //        {
        //            System.IO.File.Delete(filepathsign);
        //        }
        //        var fileName = GetUniqueName(Student.StudentSignatureActual.FileName);
        //        var uploads = Path.Combine(hostingEnvironment.WebRootPath, path);
        //        var filePath = Path.Combine(uploads, fileName);
        //        Student.StudentSignatureActual.CopyTo(new FileStream(filePath, FileMode.Create));
        //        Student.StudentSignature = "/uploads/"+Student.StudentID+"/"+fileName; // Set the file name
        //        return null;
        //    }
        //    else
        //    {
        //        return Page();
        //    }
        //}

        //private string GetUniqueName(string fileName)
        //{
        //    fileName = Path.GetFileName(fileName);
        //    return Path.GetFileNameWithoutExtension(fileName)
        //           + "_" + Guid.NewGuid().ToString().Substring(0, 4)
        //           + Path.GetExtension(fileName);
        //}
    }
}

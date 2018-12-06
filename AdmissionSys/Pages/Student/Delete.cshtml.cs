using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AdmissionSys.Pages.Student
{
    [Authorize(Roles = "Admin,Applicant")]
    public class DeleteModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public DeleteModel(AdmissionSys.Models.AdmissionSysContext context, IHostingEnvironment environment)
        {
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FindAsync(id);
            string dirpath = Student.StudentID.ToString();

            if (System.IO.Directory.Exists(hostingEnvironment.WebRootPath + "/uploads/" + dirpath))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(hostingEnvironment.WebRootPath + "/uploads/" + dirpath);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                System.IO.Directory.Delete(hostingEnvironment.WebRootPath + "/uploads/" + dirpath);
            }

            if (Student != null)
            {
                _context.Student.Remove(Student);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

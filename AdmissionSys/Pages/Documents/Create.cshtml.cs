using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using AdmissionSys.Areas.Identity.Data;
using System.IO;

namespace AdmissionSys.Pages.Documents
{
    [Authorize(Roles = "Applicant,Admin")]
    public class CreateModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CreateModel(AdmissionSys.Models.AdmissionSysContext context, IHostingEnvironment environment, UserManager<NuvAdUser> userManager)
        {
            _context = context;
            this.hostingEnvironment = environment;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Documents Documents { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));
            int stuid = sturecord.FirstOrDefault().StudentID;

            string path = hostingEnvironment.WebRootPath + "/uploads/" + stuid;
            if (Documents.DocActual != null)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = GetUniqueName(Documents.DocActual.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, path);
                var filePath = Path.Combine(uploads, fileName);
                Documents.DocActual.CopyTo(new FileStream(filePath, FileMode.Create));
                string fpath = "/uploads/" + stuid + "/" + fileName; ; // Set the file name
                return RedirectToPage("./preview", new { pathp = fileName ,doctype =Documents.DocumentType});
            }
            else
            {
                return Page();
            }
        }

           
        private string GetUniqueName(string fileName)
        {
            fileName = fileName.Replace("/", "");
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
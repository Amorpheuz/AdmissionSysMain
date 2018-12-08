using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Pages.Documents
{
    public class previewModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public previewModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            this.hostingEnvironment = environment;
        }

        public string ImagePreview;
        public string filetype;
        public string doctype1;

        public async Task<IActionResult> OnGetAsync(string pathp,string doctype)
        {
            if (pathp != null)
            {
                NuvAdUser user = await GetCurrentUserAsync();
                var sturecord = from s in _context.Student select s;
                sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));
                int stuid = sturecord.FirstOrDefault().StudentID;
                doctype1 = doctype;
                ImagePreview = "/uploads/" + stuid + "/" + pathp;
                filetype = Path.GetExtension("~/" + ImagePreview);
                return Page();
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }

        [BindProperty]
        public Models.Documents Documents { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string status = Request.Form["status"].ToString();
            string path = Request.Form["path"].ToString();
        
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));
            int stuid = sturecord.FirstOrDefault().StudentID;

            if (status == "done")
            {
                Documents.DocumentPath = path;
                Documents.StudentID = stuid;
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Documents.Add(Documents);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else if (status == "delete")
            {
                if(System.IO.File.Exists(hostingEnvironment.WebRootPath+path+""))
                {
                    System.IO.File.Delete(hostingEnvironment.WebRootPath + path + "");
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }
            }
            return null;
        }
    }
}
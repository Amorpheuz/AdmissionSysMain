using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdmissionSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AdmissionSys.Areas.Identity.Data;

namespace AdmissionSys.Pages.ApplicationList
{
    [Authorize(Roles = "Applicant,Admin")]
    public class CreateModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CreateModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet(string prg)
        {
        ViewData["ProgramsID"] = prg;
            return Page();
        }

        [BindProperty]
        public Models.ApplicationList ApplicationList { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

            ApplicationList.StudentID= sturecord.FirstOrDefault().StudentID;


            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ApplicationList.Add(ApplicationList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
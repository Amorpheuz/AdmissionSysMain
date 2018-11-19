using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Areas.Identity.Pages.Account
{
    public class confirmonregisterModel : PageModel
    {
        public confirmonregisterModel(UserManager<NuvAdUser> userManager)
        {
            this.userManager = userManager;
        }
        private readonly UserManager<NuvAdUser> userManager;
    
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (userId == null )
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            ViewData["userId"] = userId;
            return Page();
        }
    }
}

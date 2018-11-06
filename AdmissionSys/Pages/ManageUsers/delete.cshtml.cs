using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Pages.ManageUsers
{
    [Authorize(Roles = "Admin")]
    public class deleteModel : PageModel
    {
        private readonly UserManager<NuvAdUser> userManager;

        public deleteModel(UserManager<NuvAdUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            NuvAdUser userObj;
            userObj = await userManager.FindByIdAsync(id);
            IdentityResult IR1;
           
                IR1 = await userManager.DeleteAsync(userObj);

                if (IR1.Succeeded)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return RedirectToPage("../Error");
                }
        }
    }
}

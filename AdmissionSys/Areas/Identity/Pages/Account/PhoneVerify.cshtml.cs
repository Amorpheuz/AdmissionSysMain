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
    public class PhoneVerifyModel : PageModel
    {
        private readonly UserManager<NuvAdUser> userManager;

        public PhoneVerifyModel(UserManager<NuvAdUser> userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            string token = Request.Form["verifyToken"].ToString();
            IdentityResult result = await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);
            if (result.Succeeded)
            {
                return Redirect("./Index");
            }
            else
            {
                return Redirect("../../Error");
            }
        }
    }
}

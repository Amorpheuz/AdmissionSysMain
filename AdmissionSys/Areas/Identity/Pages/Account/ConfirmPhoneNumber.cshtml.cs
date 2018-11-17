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
    public class ConfirmPhoneNumberModel : PageModel
    {
        private readonly UserManager<NuvAdUser> userManager;

        public ConfirmPhoneNumberModel(UserManager<NuvAdUser> userManager)
        {
            this.userManager = userManager;
        }
        public string ConfirmResult;
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (userId == null)
            {
                return RedirectToPage("/Index");
            }
            //var user = await userManager.GetUserAsync(User);
            //if (user == null)
            //{
             //   return NotFound($"Unable to load user.");
            //}

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            if (!user.PhoneNumberConfirmed)
            {
                ViewData["ConfirmResult"] = "Thank You for confirming your email, and Phone Number. Go Ahead and <a href='./Login'> Login by clicking here</a>";
            }
            else
            {
                ViewData["ConfirmResult"] = "Thank You for confirming your Phone Number, Please cofirm your email too by clicking on a link provided to you on your provided email ID.. and then login.";
            }

            return Page();
            // var user = await userManager.GetUserAsync(User);
            //if(user.EmailConfirmed==true)
            //{
            //  ConfirmResult = "Thank You for confirming your email, and Phone Number. Go Ahead and <a href='./Login'> Login by clicking here</a>";
            //}
            //else
            //{
            //  ConfirmResult = "Thank You for confirming your Phone Number, Please cofirm your email too by clicking on a link provided to you on your provided email ID.. and then login.</a>";
            // }
        }
    }
}

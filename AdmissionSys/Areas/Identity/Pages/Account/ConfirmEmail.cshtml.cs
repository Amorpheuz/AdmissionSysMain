using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdmissionSys.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<NuvAdUser> _userManager;

        public ConfirmEmailModel(UserManager<NuvAdUser> userManager)
        {
            _userManager = userManager;
        }
        //public string ConfirmResult;


        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
           
             if(result.Succeeded && user.PhoneNumberConfirmed)
            {
            ViewData["ConfirmResult"] = "Thank You for confirming your email, and Phone Number. Go Ahead and <a href='./Login'> Login by clicking here</a>";
              
            }
            else if(result.Succeeded && !user.PhoneNumberConfirmed)
            {
                ViewData["ConfirmResult"] = "Thank You for confirming your email, please confirm your Phone Number too and try to login agian.";
             
            }
            else
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }
            return Page();
        }
    }
}

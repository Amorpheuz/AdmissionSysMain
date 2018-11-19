using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AdmissionSys.Services;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AdmissionSys.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel, ISmsSender
    {
        private readonly UserManager<NuvAdUser> _userManager;
        private readonly ISmsSender _smsSender;

        public ConfirmEmailModel(UserManager<NuvAdUser> userManager, IOptions<SMSoptions> optionsAccessor)
        {
            _userManager = userManager;
            Options = optionsAccessor.Value;
        }
        //public string ConfirmResult;
        public SMSoptions Options { get; }


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

            ViewData["userId"] = userId;
             if (result.Succeeded && user.PhoneNumberConfirmed)
            {
            ViewData["ConfirmResult"] = "Thank You for confirming your email, and Phone Number. Go Ahead and <a href='./Login'> Login by clicking here</a>";
              
            }
            else if(result.Succeeded && !user.PhoneNumberConfirmed)
            {
                ViewData["ConfirmResult"] = "Thank You for confirming your email, please confirm your Phone Number too and try to login agian.";
                 var codeSMS = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                await SendSmsAsync(user.PhoneNumber, codeSMS);
            }
            else
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }
            return Page();
        }

        public Task SendSmsAsync(string number, string message)
       {
       // Plug in your SMS service here to send a text message.
       // Your Account SID from twilio.com/console
          var accountSid = Options.SMSAccountIdentification;
       // Your Auth Token from twilio.com/console
           var authToken = Options.SMSAccountPassword;

          TwilioClient.Init(accountSid, authToken);

        return MessageResource.CreateAsync(
        to: new PhoneNumber(number),
        from: new PhoneNumber(Options.SMSAccountFrom),
         body: $"Your Verification Code is: '{message}'");
        }



        public async Task<ActionResult> OnPostAsync()
        {
            string userId = Request.Form["userId"].ToString();
            if (userId == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            string token = Request.Form["verifyToken"].ToString();
            IdentityResult result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);
            if (result.Succeeded)
            {
                return Redirect($"./ConfirmPhoneNumber?userId={userId}");
            }
            else
            {
                return Redirect("../../Error");
            }
        }
    }
}

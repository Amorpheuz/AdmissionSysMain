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
using Microsoft.AspNetCore.Identity.UI.Services;
using AdmissionSys.Services;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AdmissionSys.Pages.ApplicationList
{
    [Authorize(Roles = "Applicant,Admin")]
    public class CreateModel : PageModel, ISmsSender
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public CreateModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager, IEmailSender emailSender,IOptions<SMSoptions> optionsAccessor)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            Options = optionsAccessor.Value;
        }
        public SMSoptions Options { get; }

        public async Task<IActionResult> OnGetAsync(string prg)
        {
            if(prg==null)
            {
                return NotFound();
            }
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

            var programrec = from p in _context.ApplicationList select p;
            programrec = programrec.Where(c => c.ProgramsID == prg && c.StudentID == sturecord.FirstOrDefault().StudentID);

            if (programrec.Count() < 1)
            {
                ViewData["ProgramsID"] = prg;
                return Page();
            }
            else
            {
                return RedirectToPage("../ProgramSelect/Index");
            }
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
             body: $"{message}");
        }

        [BindProperty]
        public Models.ApplicationList ApplicationList { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            NuvAdUser user = await GetCurrentUserAsync();
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.userID.Equals(user.Id));

            ApplicationList.StudentID= sturecord.FirstOrDefault().StudentID;

            ApplicationList.Status = "Applied";
            ApplicationList.LastOpr = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ApplicationList.Add(ApplicationList);
            await _context.SaveChangesAsync();

            var programrec = from p in _context.Programs select p;
            programrec = programrec.Where(c => c.ProgramsID == ApplicationList.ProgramsID);

            await _emailSender.SendEmailAsync(user.Email, "Application Successfully Placed",
                      "Hey! Congratulation! Your application for course "+programrec.FirstOrDefault().ProgramName+" has been successfully placed. Keep a check on your mail box and DashBoard for more updates on addmission updates.");

            string smscont = "Hey! Congratulation! Your application for course " + programrec.FirstOrDefault().ProgramName + " has been successfully placed. Keep a check on your mail box and DashBoard for more updates on addmission updates.";
            await SendSmsAsync(user.PhoneNumber, smscont);

            return RedirectToPage("./Index");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
    [Authorize(Roles = "Applicant,Admin,Approver")]
    public class DeleteModel : PageModel, ISmsSender
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private Task<NuvAdUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public DeleteModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager, IEmailSender emailSender, IOptions<SMSoptions> optionsAccessor)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            Options = optionsAccessor.Value;
        }


        public SMSoptions Options { get; }

        [BindProperty]
        public Models.ApplicationList ApplicationList { get; set; }

        public string PrgName;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationList = await _context.ApplicationList
                .Include(a => a.Programs).FirstOrDefaultAsync(m => m.ApplicationListID == id);

            if (ApplicationList == null)
            {
                return NotFound();
            }
            var programrec = from p in _context.Programs select p;
            programrec = programrec.Where(c => c.ProgramsID == ApplicationList.ProgramsID);

            PrgName = programrec.FirstOrDefault().ProgramName;
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
             body: $"{message}");
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationList = await _context.ApplicationList.FindAsync(id);

            if (ApplicationList != null)
            {
                _context.ApplicationList.Remove(ApplicationList);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

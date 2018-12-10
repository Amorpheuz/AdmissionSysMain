using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using AdmissionSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AdmissionSys.Pages.Admin.Student
{
    [Authorize(Roles = "Admin,Approver")]
    public class moreoptionsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public moreoptionsModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager, IEmailSender emailSender, IOptions<SMSoptions> optionsAccessor)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
            Options = optionsAccessor.Value;
        }
        public SMSoptions Options { get; }
        [BindProperty]
        public Models.ApplicationList ApplicationList { get; set; }

        public IActionResult OnGet(int? id,int? sid,string pid)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sturec = _context.Student.Where(c => c.StudentID == id);

            ApplicationList = _context.ApplicationList.FirstOrDefault(m => m.ApplicationListID == id);

            if (ApplicationList == null)
            {
                return NotFound();
            }
            ViewData["sid"] = sid;
            ViewData["pid"] = pid;
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

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationList.StudentID= Convert.ToInt32(Request.Form["sid"]);
            ApplicationList.ProgramsID= Request.Form["pid"].ToString();

            if(ApplicationList.AdmissionConfirmed==true)
            {
                ApplicationList.Status = "Admission Confirmed";
            }
            else if(ApplicationList.ConfirmFeesPayment)
            {
                ApplicationList.Status = "Admission Confirmed";
            }
            else if(ApplicationList.AttendInterview==true)
            {
                ApplicationList.Status = "Interview Done";
            }
            else if(ApplicationList.CounsellingDone==true)
            {
                ApplicationList.Status = "Counselling Done";
            }
            else if(ApplicationList.FormVerified)
            {
                ApplicationList.Status = "Form Verified";
            }

            //if (ApplicationList.AdmissionConfirmed==true)
            //{
            //    ApplicationList.Status = "Admission Confirmed";
            //    ApplicationList.AttendInterview = true;
            //    ApplicationList.CounsellingDone = true;
            //    ApplicationList.FormVerified = true;
            //    ApplicationList.ConfirmFeesPayment = true;
            //}
            //if (ApplicationList.FormVerified == true)
            //{
            //    ApplicationList.Status = "Form Verified";
              
            //}
            //if(ApplicationList.CounsellingDone==true)
            //{
            //    ApplicationList.Status = "Counselling Done";
            //    ApplicationList.FormVerified = true;
            //}
            //if (ApplicationList.AttendInterview == true)
            //{
            //    ApplicationList.Status = "Interview Done";
            //    ApplicationList.CounsellingDone = true;
            //    ApplicationList.FormVerified = true;
            //}
            //if (ApplicationList.ConfirmFeesPayment == true)
            //{
            //    ApplicationList.Status = "Fees Paid";
            //    ApplicationList.CounsellingDone = true;
            //    ApplicationList.FormVerified = true;
            //    ApplicationList.AttendInterview = true;
            //}
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ApplicationList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationListExists(ApplicationList.ApplicationListID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await notificationAsync();

            return RedirectToPage("./Index");
        }

        private bool ApplicationListExists(int id)
        {
            return _context.ApplicationList.Any(e => e.ApplicationListID == id);
        }

        public async Task notificationAsync()
        {
            var sturecord = from s in _context.Student select s;
            sturecord = sturecord.Where(ab => ab.StudentID.Equals(ApplicationList.StudentID));
            string userid = sturecord.FirstOrDefault().userID;

            var user = _userManager.FindByIdAsync(userid);
            NuvAdUser usr = await user;
            
            //string uemail = _userManager.GetEmailAsync(usr).ToString();
            //string uphone = _userManager.GetPhoneNumberAsync(usr).ToString();
            var prg = from p in _context.Programs select p;
            prg = prg.Where(cd => cd.ProgramsID == ApplicationList.ProgramsID);
            string prgname = prg.FirstOrDefault().ProgramName;

            await _emailSender.SendEmailAsync(usr.Email, "Application " + ApplicationList.Status,
                  "Hey! Congratulation! Your application for course " + prgname + " has " + ApplicationList.Status + ". Keep a check on your mail box and DashBoard for more updates on addmission updates.");

            string smscont = "Hey! Congratulation! Your application for course " + prgname + " has " + ApplicationList.Status + ". Keep a check on your mail box and DashBoard for more updates on addmission updates.";
            await SendSmsAsync(usr.PhoneNumber, smscont);
        }


    }
}

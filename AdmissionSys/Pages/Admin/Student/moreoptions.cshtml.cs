using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdmissionSys.Pages.Admin.Student
{
    [Authorize(Roles = "Admin,Approver")]
    public class moreoptionsModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;
        private readonly UserManager<NuvAdUser> _userManager;

        public moreoptionsModel(AdmissionSys.Models.AdmissionSysContext context, UserManager<NuvAdUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        
        }

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

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationList.StudentID= Convert.ToInt32(Request.Form["sid"]);
            ApplicationList.ProgramsID= Request.Form["pid"].ToString();
            
            if(ApplicationList.AdmissionConfirmed==true)
            {
                ApplicationList.Status = "Admission Confirmed";
                ApplicationList.AttendInterview = true;
                ApplicationList.CounsellingDone = true;
                ApplicationList.FormVerified = true;
                ApplicationList.ConfirmFeesPayment = true;
            }
            if (ApplicationList.FormVerified == true)
            {
                ApplicationList.Status = "Form Verified";
              
            }
            if(ApplicationList.CounsellingDone==true)
            {
                ApplicationList.Status = "Counselling Done";
                ApplicationList.FormVerified = true;
            }
            if (ApplicationList.AttendInterview == true)
            {
                ApplicationList.Status = "Interview Done";
                ApplicationList.CounsellingDone = true;
                ApplicationList.FormVerified = true;
            }
            if (ApplicationList.ConfirmFeesPayment == true)
            {
                ApplicationList.Status = " Fees Paid";
                ApplicationList.CounsellingDone = true;
                ApplicationList.FormVerified = true;
                ApplicationList.AttendInterview = true;
            }
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

            return RedirectToPage("./Index");
        }

        private bool ApplicationListExists(int id)
        {
            return _context.ApplicationList.Any(e => e.ApplicationListID == id);
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdmissionSys.Pages.ManageUsers
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<NuvAdUser> userManager;
        private readonly AdmissionSysIdentityDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;

        public IndexModel(UserManager<NuvAdUser> userManager, AdmissionSysIdentityDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }


        public IEnumerable<NuvAdUser> usersofRoleApplicant;
        public IEnumerable<NuvAdUser> usersofRoleManager;
        public IEnumerable<NuvAdUser> usersofRoleAdmin;
        public IEnumerable<NuvAdUser> usersofRoleVerifier;
        public IEnumerable<NuvAdUser> usersofRoleFeesAcceptor;
        public IEnumerable<NuvAdUser> usersofRoleApprover;
        //  public IEnumerable<string> Roles = new List<string>();
        public IQueryable<IdentityRole> Roles;

        //  public IList<UserInfo> userInfos { get; set; }
        public async Task OnGetAsync()
        {
            //this.Users = userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToList();
            //this.Users = userManager.Users.ToList();
            //var User = await userManager.FindByIdAsync();
            //var Roles = await userManager.GetUsersInRoleAsync(User);
            // foreach (var user in userManager.Users.ToList())
            //{
            //  userInfos.Add(new UserInfo()
            //{
            //  UserId =  userManager.GetUserId(User),
            //Username = userManager.GetUserName(User),
            //Email = userManager.GetEmailAsync(user).ToString(),
            //Role =   userManager.GetUsersInRoleAsync(user).ToString()

            //});
            usersofRoleApplicant = await userManager.GetUsersInRoleAsync("Applicant");
            usersofRoleManager = await userManager.GetUsersInRoleAsync("Manager");
            usersofRoleAdmin = await userManager.GetUsersInRoleAsync("Admin");
            usersofRoleVerifier = await userManager.GetUsersInRoleAsync("Verifier");
            usersofRoleFeesAcceptor = await userManager.GetUsersInRoleAsync("FeesAcceptor");
            usersofRoleApprover = await userManager.GetUsersInRoleAsync("Approver");


            Roles = roleManager.Roles;
            // Roles.Add("Admin");
            //Roles.Add("Manager");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string uRole = Request.Form["Roles"].ToString();
            string user = Request.Form["UserID"].ToString();
            string curRole = Request.Form["curRole"].ToString();
            NuvAdUser userObj;
            userObj = await userManager.FindByIdAsync(user);
            IdentityResult IR1,IR2;
            IR2 = await userManager.RemoveFromRoleAsync(userObj, curRole);
            IR1 = await userManager.AddToRoleAsync(userObj, uRole);
            if (IR1.Succeeded && IR2.Succeeded)
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

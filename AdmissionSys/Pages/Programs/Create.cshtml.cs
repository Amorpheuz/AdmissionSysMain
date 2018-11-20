﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdmissionSys.Models;

namespace AdmissionSys.Pages.Programs
{
    public class CreateModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public CreateModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Programs Programs { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Programs.Add(Programs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
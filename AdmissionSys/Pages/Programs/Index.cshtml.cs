﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;

namespace AdmissionSys.Pages.Programs
{
    public class IndexModel : PageModel
    {
        private readonly AdmissionSys.Models.AdmissionSysContext _context;

        public IndexModel(AdmissionSys.Models.AdmissionSysContext context)
        {
            _context = context;
        }

        public IList<Models.Programs> Programs { get;set; }

        public async Task OnGetAsync()
        {
            Programs = await _context.Programs.ToListAsync();
        }
    }
}
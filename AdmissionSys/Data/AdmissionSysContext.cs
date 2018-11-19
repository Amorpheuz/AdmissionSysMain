using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdmissionSys.Models
{
    public class AdmissionSysContext : DbContext
    {
        public AdmissionSysContext (DbContextOptions<AdmissionSysContext> options)
            : base(options)
        {
        }

        public DbSet<AdmissionSys.Models.Student> Student { get; set; }
    }
}

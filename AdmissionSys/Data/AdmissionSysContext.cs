using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdmissionSys.Models;

namespace AdmissionSys.Models
{
    public class AdmissionSysContext : DbContext
    {
        public AdmissionSysContext (DbContextOptions<AdmissionSysContext> options)
            : base(options)
        {
        }

        public DbSet<AdmissionSys.Models.Student> Student { get; set; }

        public DbSet<AdmissionSys.Models.Programs> Programs { get; set; }

        public DbSet<AdmissionSys.Models.Fees> Fees { get; set; }

        public DbSet<AdmissionSys.Models.ApplicationList> ApplicationList { get; set; }

        public DbSet<AdmissionSys.Models.AcademicRecord> AcademicRecord { get; set; }

        public DbSet<AdmissionSys.Models.Documents> Documents { get; set; }
    }
}

using System;
namespace AdmissionSys.Models
{
    public class Documents
    {
        public string DocumentsID { get; set; }
        public string DocumentPath { get; set; }

        public Student Student { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AdmissionSys.Models
{
    public enum DocumentType
    {
        [Display(Name = "Proof Of Address")]
        POA,

        [Display(Name = "Caste Certificate (if applicable)")]
        CasteCerti,

        [Display(Name = "School Leaving Certificate / Transfer certificate showing date of birth")]
        SLC,

        [Display(Name = "SSC (Std. 10) or equivalent Marksheet")]
        SSCMarksheet,

        [Display(Name = "HSC (Std. 12) or equivalent Marksheet")]
        HSCMarksheet,

        [Display(Name = "HSC Attempt Certificate")]
        HSCAttemptCerti,

        [Display(Name = "Graduation/PG Marksheet and Degree Certificate (if available)")]
        GradPGMarksheet,

        [Display(Name = "Anti-ragging Undertaking by Student and Parent/Guardian")]
        AntiragUndertaking,

        [Display(Name = "Mirgration Certificate")]
        MigrateCerti,

        [Display(Name = "JEE Main Marksheet")]
        JEEMainMarksheet,

        [Display(Name = "Diploma Marksheet")]
        DiplomaMarksheet,

        [Display(Name = "HSC (Std. 12) or equivalent Passing Certificate")]
        HSCPassingCerti,

        [Display(Name = "SSC (Std. 10) or equivalent Passing Certificate")]
        SSCPassingCerti,

        [Display(Name = "NATA Score card")]
        NATAScore,

        [Display(Name = "Acknowledgement of ACPC")]
        AckACPC,

        [Display(Name = "Equivalence Certificate from Abroad Student (NRI candidate)")]
        EquiCertiNRI,

        [Display(Name = "Document for Proof Of Residence (for NRI/ NRI sponser)")]
        PORNRI,

        [Display(Name = "Attested photocopy of Passport (for NRI/ NRI sponser)")]
        AttestedPassportNRI,

        [Display(Name = "Scanned copy of Admitted Student's Passport (for NRI)")]
        PassportStudent,

        [Display(Name = "Scanned copy of Passport of Student's Mother/Father (for NRI)")]
        PassportGuardian,
    }
    public class Documents
    {
        public string DocumentsID { get; set; }
        public string DocumentPath { get; set; }

        [ScaffoldColumn(false)]
        public bool DocumentUploaded { get; set; }

        public DateTime LastOpr { get; set; }

        public DocumentType? DocumentType { get; set; }

        [NotMapped]
        [Display(Name = "Upload Your Signature")]
        //[Required(ErrorMessage = "Please Upload Your Signature")]
        public IFormFile DocActual { get; set; }

        public int StudentID { get; set; }

        public Student Student { get; set; }
    }
}
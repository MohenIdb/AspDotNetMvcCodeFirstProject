using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspDotNetMvcCodeFirstProject.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }

        public int DonorID { get; set; }
        public virtual Donor Donor { get; set; }
        public bool Status { get; set; }

        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }

        public bool IsActive { get; set; }
    }
}
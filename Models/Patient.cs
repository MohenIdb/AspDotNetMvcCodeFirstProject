using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspDotNetMvcCodeFirstProject.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string CellPhoneNo { get; set; }
        public DateTime DOB { get; set; }
        public bool IsActive { get; set; }

        public string PatientImage { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadImg { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
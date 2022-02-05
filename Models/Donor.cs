using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNetMvcCodeFirstProject.Models
{
    public class Donor
    {
        public int DonorID { get; set; }

        [Required(ErrorMessage = "Donor Name Is Required!")]
        [StringLength(20, ErrorMessage = "Name Not be Exceed 20 Character")]
        public string DonorName { get; set; }

        [Required(ErrorMessage = "Email Address Required!")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Not Valid!")]
        public string Email { get; set; }

        
        [Compare("Email", ErrorMessage = "Email Address Not Matched!")]
        public string ConfirmEmail { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Enter Donor Phone Number!")]
        [MaxLength(11, ErrorMessage = "Phone Number Can Not Exceed 11 Digits!")]
        [MinLength(11, ErrorMessage = "Some Digits Short!")]
        public string CellPhoneNo { get; set; }
        public DateTime DonationDate { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name ="BloodGroup")]
        public int BloodGroupID { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }

        public string DonorImage { get; set; }




        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
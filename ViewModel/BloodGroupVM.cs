using AspDotNetMvcCodeFirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspDotNetMvcCodeFirstProject.ViewModel
{
    public class BloodGroupVM
    {
        public int BloodGroupID { get; set; }
        public string BloodGroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donor> Donors { get; set; }
    }
}
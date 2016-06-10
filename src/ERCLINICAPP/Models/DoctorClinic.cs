using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERCLINICAPP.Models
{
    public class DoctorClinic
    {
        public Clinic Clinic { get; set; }
        public int ClinicId { get; set; }
        public ApplicationUser Doctor { get; set; }
        public string DoctorId { get; set; }
    }
}

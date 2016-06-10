using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERCLINICAPP.Models
{
    public class Clinic
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Specialty { get; set; }
        public Attending Attending { get; set; }
        public ApplicationUser Doctor { get; set; }
        [Required]
        public string Shift { get; set; }
        [Required]
        public string Insurance { get; set; }
        public ICollection<DoctorClinic> DoctorClinics { get; set; }


    }
}

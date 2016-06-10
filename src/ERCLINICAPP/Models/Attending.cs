using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERCLINICAPP.Models
{
    public class Attending
    {
        public int Id { get; set; }
        public string AttendingName { get; set; }
        public ICollection<Clinic> Clinics { get; set; }


    }
}

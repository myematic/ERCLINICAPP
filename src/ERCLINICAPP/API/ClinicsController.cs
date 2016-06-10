using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERCLINICAPP.Data;
using ERCLINICAPP.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ERCLINICAPP.API
{
    [ Route("api/[controller]")]
    public class ClinicsController : Controller
    {
        private readonly string appAdmin = "name of user";
        private ApplicationDbContext _db;
        public ClinicsController(ApplicationDbContext db)
        {
            this._db = db;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Clinic> Get()
        {

            /*check if user User.Identity.Name is Administrator */
            bool isAdmin = appAdmin == User.Identity.Name;

            if (!User.HasClaim("IsAdmin", "true"))
            {
            /* returns only clinics associated with specific doctor */
            return (from c in _db.Clinics
                          where c.DoctorClinics.Any(sc => sc.Doctor.UserName == User.Identity.Name)
                    select new Clinic
                    {
                        Id = c.Id,
                       Attending = c.Attending,
                        Insurance = c.Insurance,
                       Specialty = c.Specialty,
                       Shift = c.Shift
                    }).ToList();
            }
            else /*return all clinics for admin access */
            {
                return (from c in _db.Clinics
                       select new Clinic
                        {
                            Id = c.Id,
                            Attending = c.Attending,
                            Insurance = c.Insurance,
                            Specialty = c.Specialty,
                            Shift = c.Shift
                        }).ToList();

            }
        }

        // GET api/values/5 -getting clinic by id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Clinic ClinicToReturn = _db.Clinics.FirstOrDefault(c => c.Id == id);
            if (ClinicToReturn == null)
            {
                return BadRequest();
            }
            return Ok(ClinicToReturn);
        }

     

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Clinic Clinic)
        {

            if (Clinic.Id != 0)
            {

      

                /*Update  Insurance, shift and clinic specialty */
                var editingClinic = _db.Clinics.FirstOrDefault(m => m.Id == Clinic.Id);             
                editingClinic.Insurance = Clinic.Insurance;
                editingClinic.Shift = Clinic.Shift;
                editingClinic.Specialty = Clinic.Specialty;
                _db.SaveChanges();
                return Ok();
            }
            else
            {
                /*Add new record*/
            

            if (Clinic == null)
            {
                return BadRequest();
            }
            var attending = (from a in _db.Attendings
                             where a.Id == Clinic.Attending.Id
                             select a).FirstOrDefault();
            var doctor = (from d in _db.Users
                             where d.ResidentName == Clinic.Doctor.ResidentName
                             select d).FirstOrDefault();
            var clinicToAdd = new Clinic
            {
                Specialty = Clinic.Specialty,
                Attending = attending,
                Shift = Clinic.Shift,
                Insurance = Clinic.Insurance,
                Doctor = doctor

            };

            _db.Clinics.Add(clinicToAdd);
            _db.SaveChanges();

            var clinic = (from tbl in _db.Clinics
                          orderby tbl.Id descending
                          select tbl).FirstOrDefault();
            
            var doctorClinicAssociation = new DoctorClinic
            {
                DoctorId = doctor.Id,
                Doctor = doctor,
                Clinic = clinic,
                ClinicId = clinic.Id
            };
            _db.DoctorClinics.Add(doctorClinicAssociation);
            _db.SaveChanges();
            return Ok(Clinic);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }



        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Clinic ClinicToDelete = _db.Clinics.FirstOrDefault(c => c.Id == id);
            if (ClinicToDelete == null)
            {
                /*something weird in application*/
                return BadRequest();
            }
            _db.Clinics.Remove(ClinicToDelete);
            _db.SaveChanges();
            return Ok();
        }
     }
   }


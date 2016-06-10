using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERCLINICAPP.Data;
using ERCLINICAPP.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ERCLINICAPP.API
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            this._db = db;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Clinic> Get()
        {
            return _db.Clinics.ToList();
        }

        // GET api/values/5
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
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]Clinic clinic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {

            }
            var attending = (from a in _db.Attendings
                           where a.Id == clinic.Attending.Id
                           select a).FirstOrDefault();

            var doctor = (from d in _db.DoctorClinics
                           where d.ClinicId == clinic.Id
                           select d).FirstOrDefault();

            var ClinicToAdd = new Clinic
            {
                Specialty = clinic.Specialty,
                Attending = clinic.Attending,
                Doctor = clinic.Doctor,
                Shift = clinic.Shift,
                Insurance = clinic.Insurance,
            };
            _db.Clinics.Add(ClinicToAdd);
            _db.SaveChanges();
            return Ok(clinic);
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
            return BadRequest();
        }
        _db.Clinics.Remove(ClinicToDelete);
        _db.SaveChanges();
        return Ok();
    }

      }

    } 
  



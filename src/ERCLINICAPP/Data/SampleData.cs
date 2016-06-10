using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using ERCLINICAPP.Models;

namespace ERCLINICAPP.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Ensure Darryn (IsAdmin)
            var darryn = await userManager.FindByNameAsync("Darryn@Clinics.com");
            if (darryn == null)
            {
                // create user
                darryn = new ApplicationUser
                {
                    UserName = "Darryn@Clinics.com",
                    Email = "Darryn@Clinics.com",
                    ResidentName = "Darryn"

                };
                await userManager.CreateAsync(darryn, "Darryn123!");

                // add claims
                await userManager.AddClaimAsync(darryn, new Claim("IsAdmin", "true"));
            }

            var lynda = await userManager.FindByNameAsync("Lynda@Clinics.com");
            if (lynda == null)
            {
                // create user
                lynda = new ApplicationUser
                {
                    UserName = "Lynda@Clinics.com",
                    Email = "Lynda@Clinics.com",
                    ResidentName = "Lynda"
                };
                await userManager.CreateAsync(lynda, "Lynda123!");
            }


            // Ensure Mike (not IsAdmin)
            var dennis = await userManager.FindByNameAsync("Dennis@Clinics.com");
            if (dennis == null)
            {
                // create user
                dennis = new ApplicationUser
                {
                    UserName = "Dennis@Clinics.com",
                    Email = "Dennis@Clinics.com",
                    ResidentName = "Dennis"
                };
                await userManager.CreateAsync(dennis, "Dennis123!");
            }

            //Create Attending Physician
            var drkutter = new Attending { AttendingName = "Dr Ima Kutter MD" };
            var drfelgood = new Attending { AttendingName = "Dr I Felgood MD" };
            if (!context.Attendings.Any())
            {
                context.Attendings.AddRange(drkutter, drfelgood);
            }
            //Create Clinic
            var surgery = new Clinic { Specialty = "Surgery", Attending = drkutter, Shift = "Morning", Insurance = "BlueCross", Doctor = lynda };
            var internalmedicine = new Clinic { Specialty = "Internal Medicine", Attending = drfelgood, Shift = "Day", Insurance = "Medicare", Doctor = dennis };
            if (!context.Clinics.Any())
            {
                context.Clinics.AddRange(surgery, internalmedicine);
                context.SaveChanges();
            }
            if (!context.DoctorClinics.Any())
            {
                context.DoctorClinics.AddRange(
                    new DoctorClinic
                    {
                        DoctorId = lynda.Id,
                        ClinicId = surgery.Id
                    },

                    new DoctorClinic
                    {
                        DoctorId = dennis.Id,
                        ClinicId = internalmedicine.Id
                    }
                    );
                context.SaveChanges();
            }








        }

    }
}

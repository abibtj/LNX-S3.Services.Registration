using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration;
using S3.Services.Registration.Domain;
using System;
using System.Collections.Generic;

namespace S3.Services.Registration.Utility
{
    public class RegistrationDbInitialiser : IRegistrationDbInitialiser
    {
        private readonly RegistrationDbContext _context;

        public RegistrationDbInitialiser(RegistrationDbContext context)
            => _context = context;

        public void Initialise()
        {
            _context.Database.EnsureDeleted(); //***ToDo... remove this line later (Disasterous for production!!!)
            _context.Database.Migrate();
            _context.Database.EnsureCreated();

            SeedSchool();
            SeedSubject();
            _context.SaveChanges();
        }

        private void SeedSubject()
        {
            var subjects = new List<Subject>
            {
                new Subject { Name = "Mathematics"},
                new Subject { Name = "English"},
                new Subject { Name = "Yoruba"},
            };
            _context.Subjects.AddRange(subjects);
            //_context.SaveChanges();
        }

        private void SeedSchool()
        {
            #region Addresses
            var gwarinpa = new Address
            {
                Line1 = "75, 4th Avenue, Gwarinpa",
                Town = "Abuja",
                State = "FCT",
                Country = "Nigeria"
            };

            var ikeja = new Address
            {
                Line1 = "22, Ijoba close",
                Town = "Ikeja",
                State = "Lagos",
                Country = "Nigeria"
            };

            var mowe = new Address
            {
                Line1 = "Km 4, Lagos-Ibadan express way",
                Town = "Mowe",
                State = "Ogun",
                Country = "Nigeria"
            };

            #endregion

            #region Parents
            var mrAzeez = new Parent
            {
                FirstName = "Adejare",
                MiddleName = "Omidina",
                LastName = "Azeez",
                Gender = "Male",
                Address = gwarinpa
            };
            
            var mrOlatunji = new Parent
            {
                FirstName = "Abeeb",
                MiddleName = "Akano",
                LastName = "Olatunji",
                Gender = "Male",
                Address = ikeja
            };

            var mrsGrace = new Parent
            {
                FirstName = "Grace",
                MiddleName = "Folasade",
                LastName = "Ale",
                Gender = "Female",
                Address = mowe
            };
            #endregion

            #region Students
            var azeez1 = new Student
            {
                FirstName = "Adewale",
                MiddleName = "Qozim",
                LastName = "Azeez",
                Gender = "Male",
                Parent = mrAzeez
            };

            var azeez2 = new Student
            {
                FirstName = "Adejoke",
                MiddleName = "Fatimah",
                LastName = "Azeez",
                Gender = "Female",
                Parent = mrAzeez
            };

            var olatunji = new Student
            {
                FirstName = "Rayan",
                MiddleName = "Olamide",
                LastName = "Olatunji",
                Gender = "Male",
                Parent = mrOlatunji
            };

            var grace = new Student
            {
                FirstName = "Gwen",
                MiddleName = "Ngozi",
                LastName = "Ale",
                Gender = "Female",
                Parent = mrsGrace
            };

            #endregion

            #region Teachers
            var bob = new Teacher
            {
                FirstName = "Rayan",
                MiddleName = "Olamide",
                LastName = "Olatunji",
                Gender = "Male",
                //CreatedDate = DateTime.UtcNow,
                //UpdatedDate = DateTime.UtcNow,
                Address = new Address
                {
                    Line1 = "12, Aminu Kano Crescent",
                    Town = "Banex",
                    State = "FCT",
                    Country = "Nigeria"
                }
            };

            var bola = new Teacher
            {
                FirstName = "Aminat",
                MiddleName = "Bolanle",
                LastName = "Salimon",
                Gender = "Female",
                //CreatedDate = DateTime.UtcNow,
                //UpdatedDate = DateTime.UtcNow,
                Address = new Address
                {
                    Line1 = "Km 8, Nnamdi Azikiwe express way",
                    Town = "Gwrinpa",
                    State = "FCT",
                    Country = "Nigeria"
                }
            };

            var tunji = new Teacher
            {
                FirstName = "Abeeb",
                MiddleName = "Olatunji",
                LastName = "Liadi",
                Gender = "Male",
                //CreatedDate = DateTime.UtcNow,
                //UpdatedDate = DateTime.UtcNow,
                Address = new Address
                {
                    Line1 = "33, Tjay close",
                    Town = "Ibadan",
                    State = "Oyo",
                    Country = "Nigeria"
                }
            };
            #endregion

            #region Classes
            var pry1 = new Class
            {
                Name = "Primary 1",
                Students = new List<Student> { azeez1, grace }
            };

            var pry2 = new Class
            {
                Name = "Primary 2",
                Students = new List<Student> { azeez2 }
            };

            var nur1 = new Class
            {
                Name = "Nursery 1",
                Students = new List<Student> { olatunji }
            };

            #endregion

            #region Schools
            var bobSchool = new School
            {
                Name = "Bob Ray Group of Schools",
                Category = "Nursery",
                Classes = new List<Class> { pry1, pry2 },
                Students = new List<Student> { azeez1, azeez2, grace },
                Teachers = new List<Teacher> { bob, tunji },
                Address = new Address
                {
                    Line1 = "112, Adewale Crescent",
                    Town = "Kubwa",
                    State = "FCT",
                    Country = "Nigeria"
                }
            };

            var bolaSchool = new School
            {
                Name = "Bolanle Group of Schools",
                Category = "Primary",
                Classes = new List<Class> { nur1 },
                Students = new List<Student> { olatunji },
                Teachers = new List<Teacher> { bola },
                Address = new Address
                {
                    Line1 = "5, Pmagbe Layout",
                    Town = "Ushafa",
                    State = "FCT",
                    Country = "Nigeria"
                }
            };


            var tunjiSchool = new School
            {
                Name = "Olatunji International Colleges",
                Category = "Secondary",
                //CreatedDate = DateTime.UtcNow,
                //UpdatedDate = DateTime.UtcNow,
                Address = new Address
                {
                    Line1 = "44, Independent Avenue",
                    Town = "Osogbo",
                    State = "Osun",
                    Country = "Nigeria"
                }
            };

            var schools = new List<School>
            {
               bobSchool, bolaSchool, tunjiSchool
            };

            #endregion

            _context.Schools.AddRange(schools);
            //_context.SaveChanges();
        }
    }
}
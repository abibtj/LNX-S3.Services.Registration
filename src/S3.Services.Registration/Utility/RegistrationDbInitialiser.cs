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

            //SeedSubject();
            SeedSchool();
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
        }

        private void SeedSchool()
        {
            #region Addresses
            var gwarinpa = new ParentAddress
            {
                Line1 = "75, 4th Avenue, Gwarinpa",
                Town = "Abuja",
                State = "FCT",
                Country = "Nigeria"
            };

            var ikeja = new ParentAddress
            {
                Line1 = "22, Ijoba close",
                Town = "Ikeja",
                State = "Lagos",
                Country = "Nigeria"
            };

            var mowe = new ParentAddress
            {
                Line1 = "Km 4, Lagos-Ibadan express way",
                Town = "Mowe",
                State = "Ogun",
                Country = "Nigeria"
            };

            var oyo = new StudentAddress
            {
                Line1 = "57, Agbabiaka avenue",
                Line2 = "Mobolaje",
                Town = "Oyo",
                State = "Oyo",
                Country = "Nigeria"
            };
           
            var bwari = new ParentAddress
            {
                Line1 = "17, Pmage Layout",
                Line2 = "Ushafa",
                Town = "Bwari",
                State = "FCT",
                Country = "Nigeria"
            };

            #endregion

            #region Parents
            var mrAzeez = new Parent
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Adejare",
                MiddleName = "Omidina",
                LastName = "Azeez",
                Gender = "Male",
                Address = gwarinpa
            };
            
            var mrOlatunji = new Parent
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Abeeb",
                MiddleName = "Akano",
                LastName = "Olatunji",
                Gender = "Male",
                Address = ikeja
            };

            var mrsGrace = new Parent
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Grace",
                MiddleName = "Folasade",
                LastName = "Ale",
                Gender = "Female",
                Address = mowe
            };
           
            var mrsSalimon = new Parent
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Aminat",
                //MiddleName = "Bolanle",
                LastName = "Salimon",
                Gender = "Female",
                Address = bwari
            };
            #endregion

            #region Students
            var azeez1 = new Student
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Adewale",
                MiddleName = "Qozim",
                LastName = "Azeez",
                Gender = "Male",
                DateOfBirth = DateTime.Now,
                OfferingAllClassSubjects = true,
                Parent = mrAzeez
            };

            var azeez2 = new Student
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Adejoke",
                MiddleName = "Fatimah",
                LastName = "Azeez",
                Gender = "Female",
                DateOfBirth = DateTime.Now,
                OfferingAllClassSubjects = true,
                Parent = mrAzeez
            };

            var olatunji = new Student
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Rayan",
                MiddleName = "Olamide",
                LastName = "Olatunji",
                Gender = "Male",
                DateOfBirth = DateTime.Now,
                OfferingAllClassSubjects = true,
                Parent = mrOlatunji
            };
            
            var almas = new Student
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Almas",
                MiddleName = "Olayemi",
                LastName = "Olatunji",
                Gender = "Female",
                DateOfBirth = DateTime.Now,
                OfferingAllClassSubjects = false,
                Subjects = "Mathematics|English|Yoruba",
                Parent = mrsSalimon
            };

            var grace = new Student
            {
                RegNumber = RegNumberGenerator.Generate(),
                FirstName = "Gwen",
                MiddleName = "Ngozi",
                LastName = "Ale",
                Gender = "Female",
                DateOfBirth = DateTime.Now,
                OfferingAllClassSubjects = false,
                Subjects = "English",
                Parent = mrsGrace,
                Address = oyo
            };

            #endregion

            #region Teachers
            var bob = new Teacher
            {
                FirstName = "Rayan",
                MiddleName = "Olamide",
                LastName = "Olatunji",
                Gender = "Male",
                Address = new TeacherAddress
                {
                    Line1 = "12, Aminu Kano Crescent",
                    Town = "Banex",
                    State = "FCT",
                    Country = "Nigeria"
                },
                ScoresEntryTasks = new List<ScoresEntryTask>
                {
                    new ScoresEntryTask
                    {
                        ClassId = Guid.NewGuid(),
                        SubjectId = Guid.NewGuid()
                    },
                     new ScoresEntryTask
                    {
                        ClassId = Guid.NewGuid(),
                        SubjectId = Guid.NewGuid()
                    }
                }
            };

            var bola = new Teacher
            {
                FirstName = "Aminat",
                MiddleName = "Bolanle",
                LastName = "Salimon",
                Gender = "Female",
                Address = new TeacherAddress
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
                Address = new TeacherAddress
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
                Category = "Primary",
                Subjects = "Mathematics|English",
                Students = new List<Student> { azeez1, grace }
            };

            var pry2 = new Class
            {
                Name = "Primary 2",
                Category = "Primary",
                Subjects = "Mathematics|English",
                Students = new List<Student> { azeez2, almas }
            };

            var nur1 = new Class
            {
                Name = "Nursery 1",
                Category = "Nursery",
                Subjects = "Mathematics|English",
                Students = new List<Student> { olatunji }
            };

            #endregion

            #region Schools
            var bobSchool = new School
            {
                Name = "Bob Ray Group of Schools",
                Category = "Nursery",
                Classes = new List<Class> { pry1, pry2 },
                Students = new List<Student> { azeez1, azeez2, grace, almas },
                Teachers = new List<Teacher> { bob, tunji },
                Address = new SchoolAddress
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
                Address = new SchoolAddress
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
                Address = new SchoolAddress
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
        }
    }
}
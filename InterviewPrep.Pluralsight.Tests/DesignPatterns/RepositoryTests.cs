using InterviewPrep.DesignPatterns.DataLayer;
using InterviewPrep.DesignPatterns.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InterviewPrep.Pluralsight.Tests.DesignPatterns
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void GetStudentsTest()
        {
            DbSet<Student> students;
            IList<Student> studentsList;
            using (var dataLayer = new SchoolUnitOfWork(new SchoolContext()))
            {
                students = dataLayer.StudentRepo.GetAll() as DbSet<Student>;
                studentsList = students.ToList();
            }
            var studentsTemp = studentsList.Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            });
            Assert.AreEqual(4, studentsList.Count());
            Assert.AreEqual("Spanish", studentsList.LastOrDefault().Enrollments.FirstOrDefault().Course.Name);
        }

        [TestMethod]
        public void GetGradesTest()
        {
            var unitOfWork = new SchoolUnitOfWork(new SchoolContext());
            var businessLogic = new SchoolBusinessLogic(unitOfWork);
            var spanishClass = businessLogic.SchoolUnitOfWork.CourseRepo.Get(c => c.Name == "Spanish");
            var student = businessLogic.GetHighestGradeStudent(spanishClass.Id);
            Assert.AreEqual("Jack", student.Name);
            Assert.AreEqual(3, student.Id);
            businessLogic.Dispose();
        }
    }
}
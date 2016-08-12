using InterviewPrep.DesignPatterns;
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
            using (var dataLayer = new SchoolDataLayer(new SchoolContext()))
            {
                students = dataLayer.StudentRepository.GetAll() as DbSet<Student>;
                //studentsList = students.Include(t => t.Courses).ToList();
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
            object student;
            using (var dataLayer = new SchoolDataLayer(new SchoolContext()))
            {
                //students = dataLayer.StudentRepository.GetAll() as DbSet<Student>;
                //studentsList = students.ToList();
                student = dataLayer.GetHighestGradeStudent();
            }
            var x = 2;
        }
    }
}
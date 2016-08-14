using InterviewPrep.DesignPatterns.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public class SchoolBusinessLogic : IDisposable
    {
        public ISchoolUnitOfWork DataLayer { get; }

        public SchoolBusinessLogic(ISchoolUnitOfWork dataLayer)
        {
            DataLayer = dataLayer;
        }

        public StudentModel GetHighestGradeStudent(int courseId)
        {
            var model = DataLayer.EnrollmentRepo
                .GetAll(e => e.CourseId == courseId)
                .Include(e => e.Student)
                .OrderByDescending(e => e.Grade)
                .Select(e => new StudentModel
                {
                    Id = e.Student.Id,
                    Name = e.Student.Name
                }).FirstOrDefault();
            return model;
        }

        public void Dispose()
        {
            DataLayer.Dispose();
        }
    }
}

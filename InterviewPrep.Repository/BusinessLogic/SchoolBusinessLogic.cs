using InterviewPrep.DesignPatterns.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public class SchoolBusinessLogic : IDisposable
    {
        public ISchoolUnitOfWork SchoolUnitOfWork { get; }

        public SchoolBusinessLogic(ISchoolUnitOfWork schoolUnitOfWork)
        {
            SchoolUnitOfWork = schoolUnitOfWork;
        }

        public StudentModel GetHighestGradeStudent(int courseId)
        {
            var model = SchoolUnitOfWork.EnrollmentRepo
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
            SchoolUnitOfWork.Dispose();
        }
    }
}

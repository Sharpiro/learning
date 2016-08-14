using InterviewPrep.DesignPatterns.Entities;
using System;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public interface ISchoolUnitOfWork: IDisposable
    {
        IRepository<Course> CourseRepo { get; }
        IRepository<Student> StudentRepo { get; }
        IRepository<Enrollment> EnrollmentRepo { get; }
        void Commit();
    }
}

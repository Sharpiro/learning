using InterviewPrep.DesignPatterns.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace InterviewPrep.DesignPatterns
{
    public class SchoolDataLayer : IDisposable
    {
        private readonly DbContext _context;
        public GenericRepository<Course> CourseRepository { get; }
        public GenericRepository<Student> StudentRepository { get; }
        public GenericRepository<Enrollment> EnrollmentRepository { get; }

        public SchoolDataLayer(DbContext context)
        {
            _context = context;
            CourseRepository = new GenericRepository<Course>(_context);
            StudentRepository = new GenericRepository<Student>(_context);
            EnrollmentRepository = new GenericRepository<Enrollment>(_context);
        }

        public object GetHighestGradeStudent()
        {
            //get highest grade for spanish
            var temp1 = EnrollmentRepository.GetAll(e => e.Course.Name == "Spanish").Include(e => e.Student).OrderByDescending(e => e.Grade).FirstOrDefault();
            return new { Id = temp1.Student.Id, Name = temp1.Student.Name };
            //var temp2 = temp1.FirstOrDefault();
            //var temp = spanishEnrollment.Course.Name;
            //var students = StudentRepository.GetAll(s => s.Name == "Dave");
            //var enrollment = students.Where(s => s.Enrollments.Contains(spanishEnrollments));
            //var grade = enrollment.Grade;
            //var temp = StudentRepository.GetAll(s => s.Id == 1 && s.Enrollments);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

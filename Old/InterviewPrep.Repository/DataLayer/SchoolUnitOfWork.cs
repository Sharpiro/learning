using InterviewPrep.DesignPatterns.Entities;
using System.Data.Entity;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public class SchoolUnitOfWork : ISchoolUnitOfWork
    {
        private readonly DbContext _context;

        public IRepository<Course> CourseRepo { get; }
        public IRepository<Student> StudentRepo { get; }
        public IRepository<Enrollment> EnrollmentRepo { get; }

        public SchoolUnitOfWork(DbContext context)
        {
            _context = context;
            CourseRepo = new EFRepository<Course>(_context);
            StudentRepo = new EFRepository<Student>(_context);
            EnrollmentRepo = new EFRepository<Enrollment>(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

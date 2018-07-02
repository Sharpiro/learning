using System.Collections.Generic;

namespace InterviewPrep.DesignPatterns.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
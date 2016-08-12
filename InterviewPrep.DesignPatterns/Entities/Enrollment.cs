using System;

namespace InterviewPrep.DesignPatterns.Entities
{
    public class Enrollment : IComparable<Enrollment>
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Grade { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }

        public int CompareTo(Enrollment other)
        {
            return this.Grade.CompareTo(other.Grade);
        }
    }
}

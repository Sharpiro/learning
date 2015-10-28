using System.ComponentModel.DataAnnotations.Schema;

namespace InterviewPrep.Core.Data_Access
{
    [Table("AspNetRoles")]
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Discriminator { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace skress.Models
{
    public class WorkersTable
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
    }
}


using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;


namespace skress.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       
        public DbSet<VisitorsTable> VisitorsTable { get; set; }
        public DbSet<InstructorsTable> InstructorsTable { get; set; }
        public DbSet<DataPassTable> DataPassTable { get; set; }
        public DbSet<DataInstructorTable> dataInstructorTable { get; set; }
        public DbSet<TrackTable> TrackTable { get; set; }
        public DbSet<PassTable> PassTable { get; set; }
        public DbSet<EquipmentTable> EquipmentTable { get; set;}
        public DbSet<DataRentTable> DataRentTable { get; set;}
        public DbSet<WorkersTable> WorkersTable { get; set; }
       
    }
}

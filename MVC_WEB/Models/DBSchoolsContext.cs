using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVC_WEB.Models
{
    public partial class DBSchoolsContext : DbContext
    {
        public DBSchoolsContext()
            : base("name=DBSchoolsConnectionString")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<LoginViewModel> Students { get; set; }
        public virtual DbSet<LoginViewModel> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

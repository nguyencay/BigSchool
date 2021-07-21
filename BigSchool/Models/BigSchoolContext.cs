using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BigSchool.Models
{
    public partial class BigSchoolContext : DbContext
    {
        public BigSchoolContext()
            : base("name=BigSchoolContext2")
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coursee> Coursees { get; set; }
        public virtual DbSet<Following> Followings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coursee>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.Coursee)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);
        }
    }
}

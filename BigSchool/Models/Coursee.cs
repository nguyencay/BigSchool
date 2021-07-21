namespace BigSchool.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coursee")]
    public partial class Coursee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coursee()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }

        [StringLength(128)]
        public string LecturerId { get; set; }

        [StringLength(255)]
        public string Place { get; set; }

        public DateTime? DateTime { get; set; }

        public int? CategoryId { get; set; }
        public List<Category> ListCategory = new List<Category>();
        public string Name;
        public string LecturerName;
        public bool isLogin = false;
        public bool isShowGoing = false;
        public bool isShowFollow = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual Category Category { get; set; }
    }
}

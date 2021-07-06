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
        public int Id { get; set; }

        [StringLength(128)]
        public string LecturerId { get; set; }
        [Required]
        [StringLength(255)]
        public string Place { get; set; }

        public DateTime? DateTime { get; set; }
        [Required]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public string Name;

        public List<Category> ListCategory = new List<Category>();
    }
}

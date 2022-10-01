
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OSD.RazorData.Models.SysMapper.Tables
{
    public partial class Tag
    {
        public int TagId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [DisplayName("Enter Name: ")]
        [StringLength(256, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? LifeCycleId { get; set; }

        public virtual Category Category { get; set; }
        public virtual LifeCycle LifeCycle { get; set; }
    }
}
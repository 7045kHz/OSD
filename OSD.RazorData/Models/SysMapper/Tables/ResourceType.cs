
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OSD.RazorData.Models.SysMapper.Tables
{
    public partial class ResourceType
    {
        public int ResourceTypeId { get; set; }
        public int? LifeCycleId { get; set; }
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Vendor is Required")]
        [DisplayName("Enter Vendor: ")]
        [StringLength(256, ErrorMessage = "Vendor is too long.")]
        public string Vendor { get; set; }
        [Required(ErrorMessage = "Product is Required")]
        [DisplayName("Enter Product: ")]
        [StringLength(256, ErrorMessage = "Product is too long.")]
        public string Product { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [DisplayName("Enter Name: ")]
        [StringLength(4000, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        public virtual Category Category { get; set; }
        public virtual LifeCycle LifeCycle { get; set; }
    }
}
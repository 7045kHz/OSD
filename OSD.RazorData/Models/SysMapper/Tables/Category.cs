
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OSD.RazorData.Models.SysMapper.Tables
{
    public partial class Category
    {
        public Category()
        {
            LabelMap = new HashSet<LabelMap>();
            LabelType = new HashSet<LabelType>();
            Ou = new HashSet<Ou>();
            ResourceMap = new HashSet<ResourceMap>();
            ResourceType = new HashSet<ResourceType>();
            Tag = new HashSet<Tag>();
        }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int LifeCycleId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [DisplayName("Enter Category Name: ")]
        [StringLength(4000, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        public virtual LifeCycle LifeCycle { get; set; }
        public virtual ICollection<LabelMap> LabelMap { get; set; }
        public virtual ICollection<LabelType> LabelType { get; set; }
        public virtual ICollection<Ou> Ou { get; set; }
        public virtual ICollection<ResourceMap> ResourceMap { get; set; }
        public virtual ICollection<ResourceType> ResourceType { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}
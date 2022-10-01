
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OSD.RazorData.Models.SysMapper.Tables
{
    public partial class LifeCycle
    {
        public LifeCycle()
        {
            Category = new HashSet<Category>();
            LabelMap = new HashSet<LabelMap>();
            LabelType = new HashSet<LabelType>();
            Ou = new HashSet<Ou>();
            ResourceMap = new HashSet<ResourceMap>();
            ResourceType = new HashSet<ResourceType>();
            Tag = new HashSet<Tag>();
        }

        public int LifeCycleId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [DisplayName("Enter Name: ")]
        [StringLength(256, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<LabelMap> LabelMap { get; set; }
        public virtual ICollection<LabelType> LabelType { get; set; }
        public virtual ICollection<Ou> Ou { get; set; }
        public virtual ICollection<ResourceMap> ResourceMap { get; set; }
        public virtual ICollection<ResourceType> ResourceType { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}
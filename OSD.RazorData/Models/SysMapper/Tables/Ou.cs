
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OSD.RazorData.Models.SysMapper.Tables
{
    public partial class Ou
    {
        public Ou()
        {
            LabelType = new HashSet<LabelType>();
        }

        public int OuId { get; set; }
        [Required(ErrorMessage = "Organization is Required")]
        [DisplayName("Enter Organization: ")]
        [StringLength(256, ErrorMessage = "Organization is too long.")]
        public string Organization { get; set; }
        [Required(ErrorMessage = "Team is Required")]
        [DisplayName("Enter Team: ")]
        [StringLength(256, ErrorMessage = "Team is too long.")]
        public string Team { get; set; }
        public int? CategoryId { get; set; }
        public int? LifeCycleId { get; set; }

        public virtual Category Category { get; set; }
        public virtual LifeCycle LifeCycle { get; set; }
        public virtual ICollection<LabelType> LabelType { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace OSD.RazorData.Models.SysMapper.Tables
{
    public partial class LabelMap
    {
        public int LabelMapId { get; set; }
        public int? LabelTypeId { get; set; }
        public int? LifeCycleId { get; set; }
        public int? CategoryId { get; set; }
        public int? TagId { get; set; }

        public virtual Category Category { get; set; }
        public virtual LifeCycle LifeCycle { get; set; }
    }
}
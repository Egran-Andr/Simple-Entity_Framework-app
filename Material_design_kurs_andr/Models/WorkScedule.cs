using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class WorkScedule
    {
        public TimeSpan Endtime { get; set; }
        public TimeSpan Starttime { get; set; }
        public int Weekday { get; set; }
        public int WorkerId { get; set; }
        public DateTime WorkFrom { get; set; }
        public DateTime WorkTo { get; set; }

        public virtual Workers Worker { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class WorkersScedule
    {
        public string Days { get; set; }
        public TimeSpan Endtime { get; set; }
        public string Fio { get; set; }
        public TimeSpan Starttime { get; set; }
        public int Weekday { get; set; }
    }
}

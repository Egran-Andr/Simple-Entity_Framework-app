using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class ServiceSchedule
    {
        public int PatientId { get; set; }
        
        public int ServiceId { get; set; }
        public int WorkerId { get; set; }
        public DateTime ServiceDate { get; set; }

        public virtual Service Service { get; set; }
        public virtual Workers Worker { get; set; }
    }
}

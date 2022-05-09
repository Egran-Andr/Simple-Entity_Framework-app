using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class PatientProcedure
    {
        public DateTime? AnakisResult { get; set; }
        public DateTime AnalisDate { get; set; }
        public int AnalisType { get; set; }
        public int PatientVisit { get; set; }

        public virtual ProcedureList AnalisTypeNavigation { get; set; }
        public virtual PatientVisit PatientVisitNavigation { get; set; }
    }
}

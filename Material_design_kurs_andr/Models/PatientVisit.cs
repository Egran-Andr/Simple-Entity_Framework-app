using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class PatientVisit
    {
        public PatientVisit()
        {
            PatientProcedure = new HashSet<PatientProcedure>();
            VizitIll = new HashSet<VizitIll>();
        }

        public int DepartmentVisit { get; set; }
        public int PatWorkerId { get; set; }
        public int PatientId { get; set; }
        public DateTime VizitPatDatebegin { get; set; }
        public DateTime? VizitPatDateend { get; set; }
        public int VizitPatId { get; set; }
        public string VizitRezult { get; set; }

        public virtual HospitalDepatment DepartmentVisitNavigation { get; set; }
        public virtual Workers PatWorker { get; set; }
        public virtual ICollection<PatientProcedure> PatientProcedure { get; set; }
        public virtual ICollection<VizitIll> VizitIll { get; set; }
    }
}

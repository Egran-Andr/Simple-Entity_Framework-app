using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class Workers
    {
        public Workers()
        {
            Holidays = new HashSet<Holidays>();
            HospitalDepatment = new HashSet<HospitalDepatment>();
            PatientVisit = new HashSet<PatientVisit>();
            Records = new HashSet<Records>();
            ServiceSchedule = new HashSet<ServiceSchedule>();
            WorkScedule = new HashSet<WorkScedule>();
        }

        public int IdWorkers { get; set; }
        public int? WorkExperience { get; set; }
        public string WorkerAdress { get; set; }
        public DateTime WorkerAge { get; set; }
        public string WorkerGender { get; set; }
        public string WorkerName { get; set; }
        public string WorkerPassport { get; set; }
        public string WorkerPhone { get; set; }
        public int WorkerPosition { get; set; }
        public string WorkerSecondname { get; set; }
        public string WorkerSurname { get; set; }

        public virtual Positions WorkerPositionNavigation { get; set; }
        public virtual LoginStorage LoginStorage { get; set; }
        public virtual WorkersDepartment WorkersDepartment { get; set; }
        public virtual ICollection<Holidays> Holidays { get; set; }
        public virtual ICollection<HospitalDepatment> HospitalDepatment { get; set; }
        public virtual ICollection<PatientVisit> PatientVisit { get; set; }
        public virtual ICollection<Records> Records { get; set; }
        public virtual ICollection<ServiceSchedule> ServiceSchedule { get; set; }
        public virtual ICollection<WorkScedule> WorkScedule { get; set; }
    }
}

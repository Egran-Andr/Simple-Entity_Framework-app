using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class HospitalDepatment
    {
        public HospitalDepatment()
        {
            PatientVisit = new HashSet<PatientVisit>();
            WorkersDepartment = new HashSet<WorkersDepartment>();
        }

        public string DepartmentAdress { get; set; }
        public int DepartmentChief { get; set; }
        public string DepartmentName { get; set; }
        public int Departmentid { get; set; }
        public string Description { get; set; }

        public virtual Workers DepartmentChiefNavigation { get; set; }
        public virtual ICollection<PatientVisit> PatientVisit { get; set; }
        public virtual ICollection<WorkersDepartment> WorkersDepartment { get; set; }
    }
}

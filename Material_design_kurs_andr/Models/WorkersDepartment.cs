using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class WorkersDepartment
    {
        public int DepartmentId { get; set; }
        public int WorkerId { get; set; }

        public virtual HospitalDepatment Department { get; set; }
        public virtual Workers Worker { get; set; }
    }
}

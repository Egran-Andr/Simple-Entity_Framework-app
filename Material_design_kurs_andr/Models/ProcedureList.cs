using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class ProcedureList
    {
        public ProcedureList()
        {
            PatientProcedure = new HashSet<PatientProcedure>();
        }

        public string AnalisDiscryption { get; set; }
        public string AnalisName { get; set; }
        public int AnlisId { get; set; }

        public virtual ICollection<PatientProcedure> PatientProcedure { get; set; }
    }
}

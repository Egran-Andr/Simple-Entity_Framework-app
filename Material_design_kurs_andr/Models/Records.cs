using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class Records
    {
        public string PatientOmc { get; set; }
        public DateTime RecordDate { get; set; }
        public int WorkerRecord { get; set; }

        public virtual Workers WorkerRecordNavigation { get; set; }
    }
}

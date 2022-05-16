using System;

namespace Material_design_kurs_andr.Models
{
    public partial class Records
    {
        public int WorkerRecord { get; set; }
        public DateTime RecordDate { get; set; }
        public string PatientOmc { get; set; }

        public virtual Workers WorkerRecordNavigation { get; set; }
    }
}

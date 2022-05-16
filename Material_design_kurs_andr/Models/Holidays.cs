using System;

namespace Material_design_kurs_andr.Models
{
    public partial class Holidays
    {
        public DateTime Hday { get; set; }
        public int Idholidays { get; set; }
        public bool IsWork { get; set; }
        public int? Worker { get; set; }

        public virtual Workers WorkerNavigation { get; set; }
    }
}

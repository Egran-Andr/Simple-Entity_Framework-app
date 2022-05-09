using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class Service
    {
        public Service()
        {
            ServiceSchedule = new HashSet<ServiceSchedule>();
        }

        public string Descryption { get; set; }
        public int IdService { get; set; }
        public decimal ServiceCost { get; set; }
        public string ServiceName { get; set; }

        public virtual ICollection<ServiceSchedule> ServiceSchedule { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class PackageType
    {
        public PackageType()
        {
            Drugs = new HashSet<Drugs>();
        }

        public string Comment { get; set; }
        public string PackageStorage { get; set; }
        public string PackageType1 { get; set; }
        public int TypeId { get; set; }

        public virtual ICollection<Drugs> Drugs { get; set; }
    }
}

using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class Drugs
    {
        public Drugs()
        {
            IllProgress = new HashSet<IllProgress>();
        }

        public string DrugContradication { get; set; }
        public decimal DrugCost { get; set; }
        public string DrugIndication { get; set; }
        public string DrugName { get; set; }
        public int DrugPackage { get; set; }
        public int IdDrugs { get; set; }

        public virtual PackageType DrugPackageNavigation { get; set; }
        public virtual ICollection<IllProgress> IllProgress { get; set; }
    }
}

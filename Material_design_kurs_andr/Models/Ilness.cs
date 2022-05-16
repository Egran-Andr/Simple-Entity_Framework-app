using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class Ilness
    {
        public Ilness()
        {
            IllProgress = new HashSet<IllProgress>();
            VizitIll = new HashSet<VizitIll>();
        }

        public int IdIlness { get; set; }
        public string IllEffects { get; set; }
        public int IllLenght { get; set; }
        public string IllName { get; set; }
        public string IllSymptoms { get; set; }

        public virtual ICollection<IllProgress> IllProgress { get; set; }
        public virtual ICollection<VizitIll> VizitIll { get; set; }
    }
}

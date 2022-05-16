namespace Material_design_kurs_andr.Models
{
    public partial class VizitIll
    {
        public int IdIll { get; set; }
        public int IdVizit { get; set; }

        public virtual Ilness IdIllNavigation { get; set; }
        public virtual PatientVisit IdVizitNavigation { get; set; }
    }
}

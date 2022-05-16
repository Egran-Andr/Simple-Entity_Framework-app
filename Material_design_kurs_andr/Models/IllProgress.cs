namespace Material_design_kurs_andr.Models
{
    public partial class IllProgress
    {
        public int IllDrugId { get; set; }
        public int IllId { get; set; }

        public virtual Ilness Ill { get; set; }
        public virtual Drugs IllDrug { get; set; }
    }
}

using System;

namespace Material_design_kurs_andr.Models
{
    public partial class PatientInfo
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientSecondname { get; set; }
        public DateTime PatientAge { get; set; }
        public string PatientGender { get; set; }
        public string PatientAdress { get; set; }
        public string PatientPhone { get; set; }
        public string OmcNumber { get; set; }
        public string SnilsNumber { get; set; }
        public string DmcOrganisation { get; set; }
        public string DmcNumber { get; set; }

    }
}

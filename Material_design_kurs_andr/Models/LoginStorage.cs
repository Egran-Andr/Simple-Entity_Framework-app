using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class LoginStorage
    {
        public string AccountVerification { get; set; }
        public string Password { get; set; }
        public int WorkerId { get; set; }

        public virtual Workers Worker { get; set; }
    }
}

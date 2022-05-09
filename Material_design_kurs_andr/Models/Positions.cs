using System;
using System.Collections.Generic;

namespace Material_design_kurs_andr.Models
{
    public partial class Positions
    {
        public Positions()
        {
            Workers = new HashSet<Workers>();
        }

        public int IdPositions { get; set; }
        public string PositionName { get; set; }
        public string PositionRequirements { get; set; }
        public string PositionResponsibility { get; set; }
        public decimal PositionSalary { get; set; }

        public virtual ICollection<Workers> Workers { get; set; }
    }
}

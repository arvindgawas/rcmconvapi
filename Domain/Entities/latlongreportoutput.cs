using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class latlongreportoutput
    {
     
        [Key]

        public string Location { get; set; }
        public string custodian { get; set; }
        public string EmpName { get; set; }
        public decimal kmdistance { get; set; }

    }
}

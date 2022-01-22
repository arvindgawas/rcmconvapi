using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class latlonglistbool
    {
        [Key]
        public decimal callno { get; set; }
        public DateTime gendate { get; set; }
        public string region { get; set; }
        public string location { get; set; }
        public string hublocation { get; set; }
        public string custcustomercode { get; set; }
        public string callstatus { get; set; }
        public Boolean flagAllowLocException { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class customermaster
    {
        [Key]
        public string customercode { get;set;}

        public string customername { get; set; }

        public decimal? ncmbillingrate { get; set; }

    }
}



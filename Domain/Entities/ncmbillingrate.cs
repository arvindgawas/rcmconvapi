using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ncmbillingrate
    {
        [Key]
        public string clientcustcode { get; set; }
        public string clientcustname { get; set; }
        public string customername { get; set; }
        public string regionname { get; set; }
        public string locationname { get; set; }
        public decimal? billingrate { get; set; }
    }
}

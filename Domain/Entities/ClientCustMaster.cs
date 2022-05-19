using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ClientCustMaster
    {
        [Key]
        public string clientcustcode { get; set; }
        public string clientcustname { get; set; }
        public string customercode { get; set; }
        public DateTime? enddate { get; set; }
        public string regioncode { get; set; }
        public string locationcode { get; set; }
        public decimal? billingrate { get; set; }

    }
}




using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class businesscalllogdetails
    {
        [Key]
        public decimal callno { get; set; }
        public DateTime actiondate { get; set; }
        public string regionname { get; set; }
        public string locationname { get; set; }
        public string hublocationname { get; set; }
        public string clientcustname { get; set; }
      
    }
}

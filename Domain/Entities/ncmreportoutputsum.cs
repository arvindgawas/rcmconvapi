using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
   
    public class ncmreportoutputsum
    {
        [Key]
        public string Customer { get; set; }
        public string ClientName { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string Hublocation { get; set; }
        public decimal? DepositedAmount { get; set; }
        public decimal? PartnerBankRate { get; set; }
        public decimal? BillingRate { get; set; }
        public decimal? NCMCost { get; set; }
        public decimal? BillingCost { get; set; }
    }

}

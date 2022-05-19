using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ncmaccountmaster
    {
        public string customercode { get; set; }
        public string customerbranchcode { get; set; }

        [Key]
        public string benificiaryaccountnumber { get; set; }
        public decimal? PartnerBankRate { get; set; }
    }
}

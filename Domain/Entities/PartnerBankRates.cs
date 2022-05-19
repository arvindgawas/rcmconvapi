using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PartnerBankRates
    {
        public string regionname { get; set; }

        public string locationname { get; set; }

        public string hublocationname { get; set; }

        public string customercode { get; set; }

        public string customername { get; set; }

        [Key]
        public string customerbranchcode { get; set; }

        public string customerbranchname { get; set; }

        public decimal? PartnerBankRate { get; set; }
    }
}





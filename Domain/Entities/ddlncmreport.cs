using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ddlncmreport
    {
        public List<regionmast> lstregionmast { get; set; }
        public List<locationmast> lstlocationmast { get; set; }
        public List<hublocationmast> lsthublocationmast { get; set; }
        public List<customermaster> lstcustomer { get; set; }
        public List<ClientCustMaster> lstcrn { get; set; }

    }
}

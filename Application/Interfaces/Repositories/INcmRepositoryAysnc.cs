using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface INcmRepositoryAysnc : IGenericRepositoryAsync<customermaster>
    {
        Task<ddlncmreport> getddlistreport(string region,string location,string customer);
        Task<List<ncmreportoutput>> getNcmReport(string region, string location, string customer, string fromdate, string todate,string scrn,string reporttype);
        Task<List<ncmreportoutputsum>> getNcmReportSum(string region, string location, string customer, string fromdate, string todate, string scrn, string reporttype);
        Task<List<ncmbillingrate>> GetCustomer();
        Task<List<PartnerBankRates>> GetPartnerBank();
        void updatePartnerBankRates(List<CustomerBranchMaster> objCustomerBranchMaster);
        void updateCustomerRates(List<customermaster> objcustomer);
    }
}

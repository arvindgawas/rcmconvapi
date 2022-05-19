using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistence.Repositories
{
    public class NcmRepositoryAysnc : GenericRepositoryAsync<customermaster>, INcmRepositoryAysnc
    {
       
        private readonly ApplicationDbContext _dbContext;

        public NcmRepositoryAysnc(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ddlncmreport> getddlistreport(string region,string location,string customer)
        {
            ddlncmreport ddl = new ddlncmreport();

            var regionlist = (from e in _dbContext.regionmast
                              select new regionmast
                              {
                                  regioncode = e.regioncode,
                                  regionname = e.regionname
                              }).ToList();

            ddl.lstregionmast = regionlist;

            var customerlist = (from e in _dbContext.customermaster
                                select new customermaster
                                {
                                    customercode = e.customercode,
                                    customername = e.customername
                                }).ToList();

            ddl.lstcustomer = customerlist;

            if (!string.IsNullOrEmpty(region))
            {
                var locationlist = (from a in _dbContext.locationmast
                                    where a.regioncode == region
                                    select new locationmast
                                    {
                                        locationcode = a.locationcode,
                                        locationname = a.locationname
                                    }).ToList();

                ddl.lstlocationmast = locationlist;

            }

            var hublocationlist = (from a in _dbContext.hublocationmast
                                   where a.hublocationcode == "1001"
                                select new hublocationmast
                                {
                                    hublocationcode = a.hublocationcode,
                                    hublocationname = a.hublocationname
                                }).ToList();

            ddl.lsthublocationmast = hublocationlist;

            if (!string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(customer))
            {
                var crnlist = (from a in _dbContext.ClientCustMaster
                               where a.locationcode == location && a.customercode == customer
                               select new ClientCustMaster
                               {
                                   clientcustcode = a.clientcustcode,
                                   clientcustname = a.clientcustname
                               }).ToList();

                ddl.lstcrn = crnlist;
            }    

            var m = await Task.Run(() => ddl);


            return m;
        }

        public virtual async Task<List<ncmreportoutput>> getNcmReport(string region, string location, string customer, string fromdate, string todate,string scrn,string reporttype)
        {
            
            if (customer==null)
            {
                customer = "undefined";
            }

            if (location == null)
            {
                location = "undefined";
            }

            var customerparam = new SqlParameter("@customer", customer);
            var locationparam = new SqlParameter("@location", location);
            var fromdateparam = new SqlParameter("@fromdate", fromdate);
            var todateparam = new SqlParameter("@todate", todate);
            var type = new SqlParameter("@type", reporttype);
            var crn = new SqlParameter("@crn", scrn);
           
                var lstncmreportoutput = _dbContext
                       .ncmreportoutput
                       .FromSqlRaw("exec sp_ncmcostingreport @customer, @location,@fromdate,@todate,@type,@crn", customerparam, locationparam, fromdateparam, todateparam, type, crn)
                       .ToList();

                var m = await Task.Run(() => lstncmreportoutput);

                return m.ToList();
        }

        public virtual async Task<List<ncmreportoutputsum>> getNcmReportSum(string region, string location, string customer, string fromdate, string todate, string scrn, string reporttype)
        {

            if (customer == null)
            {
                customer = "undefined";
            }

            if (location == null)
            {
                location = "undefined";
            }

            var customerparam = new SqlParameter("@customer", customer);
            var locationparam = new SqlParameter("@location", location);
            var fromdateparam = new SqlParameter("@fromdate", fromdate);
            var todateparam = new SqlParameter("@todate", todate);
            var type = new SqlParameter("@type", reporttype);
            var crn = new SqlParameter("@crn", scrn);

            var lstncmreportoutput = _dbContext
                   .ncmreportoutputsum
                   .FromSqlRaw("exec sp_ncmcostingreport @customer, @location,@fromdate,@todate,@type,@crn", customerparam, locationparam, fromdateparam, todateparam, type, crn)
                   .ToList();

            var m = await Task.Run(() => lstncmreportoutput);

            return m.ToList();
        }

        public virtual async Task<List<PartnerBankRates>> GetPartnerBank()
        {
            var lstPartnerBankRates = _dbContext
                    .PartnerBankRates
                    .FromSqlRaw("exec sp_getncmaccountrates")
                    .ToList();

            var m = await Task.Run(() => lstPartnerBankRates);

            return m.ToList();
        }

        public void updatePartnerBankRates(List<CustomerBranchMaster> objCustomerBranchMaster)
        {
            foreach (CustomerBranchMaster tmptilist in objCustomerBranchMaster)
            {
                CustomerBranchMaster tc = (from c in _dbContext.CustomerBranchMaster
                                           where c.customerbranchcode == tmptilist.customerbranchcode && c.customercode == tmptilist.customercode
                                       select c).FirstOrDefault();

                if (tmptilist.PartnerBankRate > 0)
                {
                    tc.PartnerBankRate = tmptilist.PartnerBankRate;
                    _dbContext.Entry(tc).State = EntityState.Modified;
                }

                
            }
        }

        /*
        public virtual async Task<List<customermaster>> GetCustomer()
        {
            var tc = (from a in _dbContext.customermaster
                      select new customermaster
                      {
                          customercode = a.customercode,
                          customername = a.customername,
                          ncmbillingrate = a.ncmbillingrate
                      }).ToList();

            var m = await Task.Run(() => tc);

            return m.ToList();
        }
        */

        public virtual async Task<List<ncmbillingrate>> GetCustomer()
        {
            // 
            var tc = (from a in _dbContext.ClientCustMaster
                      join c in _dbContext.customermaster on a.customercode equals c.customercode
                      join r in _dbContext.regionmast on a.regioncode equals r.regioncode
                      join l in _dbContext.locationmast on a.locationcode equals l.locationcode
                      join b in _dbContext.clientcustbankdetails on a.clientcustcode equals b.clientcustcode
                      where b.depositiontype == "NCM" && a.enddate == null
                      select new ncmbillingrate
                      {
                          clientcustcode = a.clientcustcode,
                          clientcustname = a.clientcustname,
                          customername = c.customername,
                          regionname = r.regionname,
                          locationname =l.locationname,
                          billingrate = a.billingrate
                      }).ToList();

            var m = await Task.Run(() => tc);

            return m.ToList();
        }

        public void updateCustomerRates(List<customermaster> objcustomer)
        {
            foreach (customermaster tmptilist in objcustomer)
            {


                ClientCustMaster tc = (from c in _dbContext.ClientCustMaster
                                     where c.clientcustcode == tmptilist.customercode
                                       select c).FirstOrDefault();

                if (tmptilist.ncmbillingrate>0)
                {
                    tc.billingrate = tmptilist.ncmbillingrate;
                    _dbContext.Entry(tc).State = EntityState.Modified;
                }
               
            }
            //_dbContext.SaveChangesAsync();
        }

    }
}

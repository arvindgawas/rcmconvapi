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
    public class LatLongRepositoryAysnc : GenericRepositoryAsync<latlonglist>, ILatlongRepository
    {
        private readonly DbSet<latlonglist> _latlonglist;
        private readonly ApplicationDbContext _dbContext;

        public LatLongRepositoryAysnc(ApplicationDbContext dbContext) : base(dbContext)
        {
            _latlonglist = dbContext.Set<latlonglist>();
            _dbContext = dbContext;
        }

        public virtual async Task<List<latlonglist>> GetLatlongException(DateTime dtgendate, string UserId)
        {
            var tc = (from a in _dbContext.businesscalllog
                      join b in _dbContext.userlocationrel on a.location equals b.LocationCode
                      join c in _dbContext.businesscalllogdetails on a.callno equals c.callno
                      where a.gendate == dtgendate && b.UserID == UserId
                      select new latlonglist
                      {
                          callno = a.callno,
                          gendate = a.gendate,
                          region = c.regionname,
                          location = c.locationname,
                          hublocation = c.hublocationname,
                          custcustomercode = a.custcustomercode,
                          clientname = c.clientcustname,
                          callstatus = a.callstatus,
                          flagAllowLocException = a.flagAllowLocException
                      }).ToListAsync();

            return await tc;
            //return  await _latlonglist.Where(x => x.gendate == dtgendate && x.location == "1087" && x.callstatus== "Assign" ).ToListAsync();

        }

        public virtual async Task<List<latlonglist>> GetLatlongExceptionnew(DateTime dtgendate, string UserId)
        {
            var tc = (from a in _dbContext.businesscalllog
                      join b in _dbContext.userlocationrel on a.location equals b.LocationCode
                      join c in _dbContext.businesscalllogdetails on a.callno equals c.callno
                      where a.gendate == dtgendate && a.gendate == c.actiondate && a.callstatus == "Assign" && b.UserID == UserId
                      select new latlonglist
                      {
                          callno = a.callno,
                          gendate = a.gendate,
                          region = c.regionname,
                          location = c.locationname,
                          hublocation = c.hublocationname,
                          custcustomercode = a.custcustomercode,
                          clientname = c.clientcustname,
                          callstatus = a.callstatus,
                          flagAllowLocException = a.flagAllowLocException
                      }).ToList();

            var result = tc.GroupBy(i => i.callno).Select(i => i.FirstOrDefault()).ToList();

            var m = await Task.Run(() => result);

            return m.ToList();
        }


        public void updatelatlonglist(List<latlonglistbool> objlatlonglists)
        {
            foreach (latlonglistbool tmptilist in objlatlonglists)
            {
                latlonglistbcl tc = (from c in _dbContext.businesscalllog
                                  where c.callno == tmptilist.callno
                                  select c).FirstOrDefault();
                if (tmptilist.flagAllowLocException)
                {
                    tc.flagAllowLocException = "1";
                }
                else
                {
                    tc.flagAllowLocException = "0";
                }

                _dbContext.Entry(tc).State = EntityState.Modified;
            }
            //_dbContext.SaveChangesAsync();
        }

        public async Task<List<latlongreportoutput>> GetLatlongReport(latlongreport objlatlongreport)
        {
            var fromdateparam = new SqlParameter("@fromdate", objlatlongreport.fromdate);
            var todateparam = new SqlParameter("@todate", objlatlongreport.todate);
            var locationparam = new SqlParameter("@location", objlatlongreport.location);

            var lstlatlongreportoutput = _dbContext
                        .latlongreportoutput
                        .FromSqlRaw("exec sp_latlongreportnew @fromdate, @todate, @location", fromdateparam, todateparam, locationparam)
                        .ToList();

            var m = await Task.Run(() => lstlatlongreportoutput);
          
            return m.ToList();
        }


        public async Task<ddllistreport> getddlistreport(string region)
        {
            ddllistreport ddl = new ddllistreport();

            var regionlist = (from e in _dbContext.regionmast
                               select new regionmast
                               {
                                   regioncode = e.regioncode,
                                   regionname = e.regionname
                               }).ToList();
            
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

            ddl.lstregionmast = regionlist;
           
            var m = await Task.Run(() => ddl);


            return m;
        }

    }
}


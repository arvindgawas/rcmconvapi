using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        //private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            Database.SetCommandTimeout(150000);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<latlonglistbcl> businesscalllog { get; set; }
        public DbSet<userlocationrel> userlocationrel { get; set; }
        public DbSet<businesscalllogdetails>  businesscalllogdetails { get; set; }
        public DbSet<latlongreportoutput> latlongreportoutput { get; set; }
        public DbSet<regionmast> regionmast { get; set; }
        public DbSet<locationmast> locationmast { get; set; }
        public DbSet<hublocationmast> hublocationmast { get; set; }
        public DbSet<customermaster> customermaster { get; set; }
        public DbSet<PartnerBankRates> PartnerBankRates { get; set; }
        public DbSet<ncmreportoutput> ncmreportoutput { get; set; }
        public DbSet<ncmreportoutputsum> ncmreportoutputsum { get; set; }
        public DbSet<CustomerBranchMaster> CustomerBranchMaster { get; set; }
        public DbSet<ClientCustMaster> ClientCustMaster { get; set; }
        public DbSet<clientcustbankdetails> clientcustbankdetails { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        //entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        //entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }

            builder.Entity<CustomerBranchMaster>().HasKey(ba => new { ba.customercode, ba.customerbranchcode });

            base.OnModelCreating(builder);
        }
    }
}

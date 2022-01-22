using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Common;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<UserMaster>, IUserRepositoryAsync
    {
        private readonly DbSet<UserMaster> _users;


        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<UserMaster>();
        }

        public virtual async Task<UserMaster> GetByIdStrAsync(UserMaster obum)
        {

            var password = Helper.EncryptText(obum.UserPassword, "MAKV2SPBNI99212");

            return await _users.Where(x => x.LoginID == obum.LoginID && x.UserPassword == password).SingleOrDefaultAsync(); 
           
        }


    }

}

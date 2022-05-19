using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitofWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductRepositoryAsync ProductRepositoryAsync => new ProductRepositoryAsync(_dbContext);
        public ILatlongRepository LatLongRepositoryAysnc => new LatLongRepositoryAysnc(_dbContext);
        public INcmRepositoryAysnc NcmRepositoryAysnc => new NcmRepositoryAysnc(_dbContext);
        public async Task<bool> Complete()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }
    }
}

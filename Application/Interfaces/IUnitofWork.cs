using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitofWork
    {
        IProductRepositoryAsync ProductRepositoryAsync { get; }
        ILatlongRepository LatLongRepositoryAysnc { get; }
        INcmRepositoryAysnc NcmRepositoryAysnc { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}

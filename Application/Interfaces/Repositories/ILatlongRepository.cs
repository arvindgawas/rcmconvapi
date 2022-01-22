using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ILatlongRepository : IGenericRepositoryAsync<latlonglist>
    {
        Task<List<latlonglist>> GetLatlongException(DateTime dtgendate,string UserId);
        Task<List<latlonglist>> GetLatlongExceptionnew(DateTime dtgendate, string UserId);
        void updatelatlonglist(List<latlonglistbool> objlatlonglists);
        Task<List<latlongreportoutput>> GetLatlongReport(latlongreport objlatlongreport);
        Task<ddllistreport> getddlistreport(string region);
    }
}




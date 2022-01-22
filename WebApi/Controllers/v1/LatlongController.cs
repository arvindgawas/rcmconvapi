using Application.Features.LatLongs.Commands.UpdateLatlong;
using Application.Features.LatLongs.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fingers10.ExcelExport.ActionResults;
using System.Linq;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LatlongController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetLatLongException(string gendate, string UserId)
        {
            DateTime dt = DateTime.Parse(gendate);
            var user = await Mediator.Send(new getLatLongQuery { gendate = dt, UserId = UserId });

            return Ok(user);
        }

        [HttpPost("UpdateLatlong")]
        public async Task<IActionResult> UpdateLatlong([FromBody] List<latlonglistbool> objlst)
        //public async Task<IActionResult> UpdateLatlong(UpdateLatlongCommand command)
        {
            var user = await Mediator.Send(new UpdateLatlongCommand { lstlatlong = objlst });

            return Ok(user);
        }

        [HttpGet("GetLatlongReport")]

        public async Task<IActionResult> GetLatlongReport(string fromdate, string todate, string user, string region, string location)
        {
            DateTime dt = DateTime.Parse(fromdate);
            DateTime dtto = DateTime.Parse(todate);
            var objlatlongreport = await Mediator.Send(new getLatLongreportQuery { fromdate = dt, todate = dtto, user = user, region = region, location = location });

        
            return Ok(objlatlongreport);
        }


        [HttpGet("GetLatlongReportnew")]

        public async Task<IActionResult> GetLatlongReportnew(string fromdate, string todate, string user, string region, string location)
        {
            DateTime dt = DateTime.Parse(fromdate);
            DateTime dtto = DateTime.Parse(todate);
            var objlatlongreport = await Mediator.Send(new getLatLongreportQuery { fromdate = dt, todate = dtto, user = user, region = region, location = location });

            var t = objlatlongreport.Data;

            var m = t.AsEnumerable();

            return new ExcelResult<latlongreportoutput>(m, "demo","test");
 
            
        }


        


        [HttpGet("getddlistreport")]
        public async Task<IActionResult>  getddlistreport(string region)
        {
            
            var ddl = await Mediator.Send(new getLatlongddlQuery { region = region } );

            return Ok(ddl);
        }

    }
}

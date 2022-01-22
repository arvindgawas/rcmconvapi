using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Features.LatLongs.Queries
{
    public class getLatLongreportQuery : IRequest<Response<List<latlongreportoutput>>>
    {
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public string user { get; set; }
        public string region { get; set; }
        public string location { get; set; }

        public class getLatLongreportQueryHandler : IRequestHandler<getLatLongreportQuery, Response<List<latlongreportoutput>>>
        {
            private readonly ILatlongRepository _LatLongRepository;
            private readonly IMapper _mapper;
            public getLatLongreportQueryHandler(ILatlongRepository LatLongRepository, IMapper mapper)
            {
                _LatLongRepository = LatLongRepository;
                _mapper = mapper;
            }


            public async Task<Response<List<latlongreportoutput>>> Handle(getLatLongreportQuery query, CancellationToken cancellationToken)
            {
                var latlonobj = _mapper.Map<latlongreport>(query);

                var latlong = await _LatLongRepository.GetLatlongReport(latlonobj);

                if (latlong == null) throw new ApiException($"latlong Not Found.");

                return new Response<List<latlongreportoutput>>(latlong);
            }


        }
    }

}

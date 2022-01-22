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
    public class getLatLongQuery : IRequest<Response<List<latlonglist>>>
    {
        public DateTime gendate { get; set; }
        public string UserId { get; set; }

        public class getLatLongQueryHandler : IRequestHandler<getLatLongQuery, Response<List<latlonglist>>>
        {
            private readonly ILatlongRepository _LatLongRepository;
            private readonly IMapper _mapper;
            public getLatLongQueryHandler(ILatlongRepository LatLongRepository, IMapper mapper)
            {
                _LatLongRepository = LatLongRepository;
                _mapper = mapper;
            }
            
            public async Task<Response<List<latlonglist>>> Handle(getLatLongQuery query, CancellationToken cancellationToken)
            {
                //var userob = _mapper.Map<latlonglist>(query);

                var latlong = await _LatLongRepository.GetLatlongExceptionnew(query.gendate, query.UserId);

                if (latlong == null) throw new ApiException($"latlong Not Found.");

                return new Response<List<latlonglist>>(latlong);
            }

            /*
            public async Task<Response<List<latlonglist>>> Handle(getLatLongQuery query, CancellationToken cancellationToken)
            {
                //var userob = _mapper.Map<latlonglist>(query);

                var latlong = await _LatLongRepository.GetLatlongException(query.gendate,query.UserId);

                if (latlong == null) throw new ApiException($"latlong Not Found.");

                return new Response<List<latlonglist>>(latlong);
            }
            */

        }
    }
}







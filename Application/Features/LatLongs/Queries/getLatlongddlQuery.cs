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
    public class getLatlongddlQuery : IRequest<Response<ddllistreport>>
    {
        public string region { get; set; }
        public class getLatlongddlQueryHandler : IRequestHandler<getLatlongddlQuery, Response<ddllistreport>>
        {
            private readonly ILatlongRepository _LatLongRepository;
            private readonly IMapper _mapper;
            public getLatlongddlQueryHandler(ILatlongRepository LatLongRepository, IMapper mapper)
            {
                _LatLongRepository = LatLongRepository;
                _mapper = mapper;
            }
            public async Task<Response<ddllistreport>> Handle(getLatlongddlQuery query, CancellationToken cancellationToken)
            {
                //var userob = _mapper.Map<latlonglist>(query);

                var latlong = await _LatLongRepository.getddlistreport(query.region);

                if (latlong == null) throw new ApiException($"latlong Not Found.");

                return new Response<ddllistreport>(latlong);
            }

        }

    }
}

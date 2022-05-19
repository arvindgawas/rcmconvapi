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

namespace Application.Features.ncm.Queries
{
    public class getNcmReportddlQuery : IRequest<Response<ddlncmreport>>
    {
        public string region { get; set; }
        public string location { get; set; }
        public string customer { get; set; }


        public class getNcmReportddlQueryHandler : IRequestHandler<getNcmReportddlQuery, Response<ddlncmreport>>
        {
            private readonly INcmRepositoryAysnc _NcmRepositoryAysnc;
            private readonly IMapper _mapper;
            public getNcmReportddlQueryHandler(INcmRepositoryAysnc NcmRepositoryAysnc, IMapper mapper)
            {
                _NcmRepositoryAysnc = NcmRepositoryAysnc;
                _mapper = mapper;
            }

            public async Task<Response<ddlncmreport>> Handle(getNcmReportddlQuery query, CancellationToken cancellationToken)
            {
                //var userob = _mapper.Map<latlonglist>(query);

                var ddl = await _NcmRepositoryAysnc.getddlistreport(query.region,query.location,query.customer);

                if (ddl == null) throw new ApiException($"ddl Not Found.");

                return new Response<ddlncmreport>(ddl);
            }
        }

    }
}



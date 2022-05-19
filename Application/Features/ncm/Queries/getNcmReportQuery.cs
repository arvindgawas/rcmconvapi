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
    public class getNcmReportQuery : IRequest<Response<List<ncmreportoutput>>>
    {
        public string region { get; set; }
        public string location { get; set; }
        public string customer { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string crn { get; set; }
        public string reporttype { get; set; }

        public class getNcmReportQueryHandler : IRequestHandler<getNcmReportQuery, Response<List<ncmreportoutput>>>
        {
            private readonly INcmRepositoryAysnc _NcmRepository;
            private readonly IMapper _mapper;
            public getNcmReportQueryHandler(INcmRepositoryAysnc NcmRepositor, IMapper mapper)
            {
                _NcmRepository = NcmRepositor;
                _mapper = mapper;
            }


            public async Task<Response<List<ncmreportoutput>>> Handle(getNcmReportQuery query, CancellationToken cancellationToken)
            {

                var customer = await _NcmRepository.getNcmReport(query.region,query.location,query.customer,query.fromdate,query.todate,query.crn,query.reporttype);

                if (customer == null) throw new ApiException($"customer Not Found.");

                return new Response<List<ncmreportoutput>>(customer);
            }

        }

    }
}



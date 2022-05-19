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
    public class GetPartnerBankRatesQuery : IRequest<Response<List<PartnerBankRates>>>
    {
        public class GetPartnerBankRatesQueryHandler : IRequestHandler<GetPartnerBankRatesQuery, Response<List<PartnerBankRates>>>
        {
            private readonly INcmRepositoryAysnc _NcmRepository;
            private readonly IMapper _mapper;
            public GetPartnerBankRatesQueryHandler(INcmRepositoryAysnc NcmRepositor, IMapper mapper)
            {
                _NcmRepository = NcmRepositor;
                _mapper = mapper;
            }

            public async Task<Response<List<PartnerBankRates>>> Handle(GetPartnerBankRatesQuery query, CancellationToken cancellationToken)
            {

                var customer = await _NcmRepository.GetPartnerBank();

                if (customer == null) throw new ApiException($"Partner Bank Not Found.");

                return new Response<List<PartnerBankRates>>(customer);
            }
        }
    }
}



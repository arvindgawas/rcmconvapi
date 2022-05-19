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
    public class getCustomerQuery : IRequest<Response<List<ncmbillingrate>>>
    {
        public class getCustomerQueryHandler : IRequestHandler<getCustomerQuery, Response<List<ncmbillingrate>>>
        {
            private readonly INcmRepositoryAysnc _NcmRepository;
            private readonly IMapper _mapper;
            public getCustomerQueryHandler(INcmRepositoryAysnc NcmRepositor, IMapper mapper)
            {
                _NcmRepository = NcmRepositor;
                _mapper = mapper;
            }


            public async Task<Response<List<ncmbillingrate>>> Handle(getCustomerQuery query, CancellationToken cancellationToken)
            {

                var customer = await _NcmRepository.GetCustomer();

                if (customer == null) throw new ApiException($"customer Not Found.");

                return new Response<List<ncmbillingrate>>(customer);
            }


        }
    }
}

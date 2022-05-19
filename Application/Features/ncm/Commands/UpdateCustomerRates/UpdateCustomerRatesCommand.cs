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
using Application.Interfaces;

namespace Application.Features.ncm.Commands.UpdateCustomerRates
{
    public class UpdateCustomerRatesCommand : IRequest<Response<int>>
    {
        public List<customermaster> Customerlst { get; set; }

        public class UpdateCustomerRatesCommandHandler : IRequestHandler<UpdateCustomerRatesCommand, Response<int>>
        {

            private readonly IMapper _mapper;
            private readonly INcmRepositoryAysnc _NcmRepository;
            private readonly IUnitofWork _unitofWork;
            public UpdateCustomerRatesCommandHandler(IUnitofWork unitofWork, IMapper mapper)
            {
                _mapper = mapper;
                _unitofWork = unitofWork;
                _NcmRepository = _unitofWork.NcmRepositoryAysnc;
            }
            public async Task<Response<int>> Handle(UpdateCustomerRatesCommand command, CancellationToken cancellationToken)
            {
                _NcmRepository.updateCustomerRates(command.Customerlst);
                await _unitofWork.Complete();
                return new Response<int>(1);
            }

        }
    }
}

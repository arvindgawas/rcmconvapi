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
using Application.Interfaces.Repositories;

namespace Application.Features.ncm.Commands.UpdatePartnerBankRates
{
    public class UpdatePartnerBankRatesCommand : IRequest<Response<int>>
    {
        public List<CustomerBranchMaster> CustomerBranchMasterlst { get; set; }

        public class UpdatePartnerBankRatesCommandHandler : IRequestHandler<UpdatePartnerBankRatesCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly INcmRepositoryAysnc _NcmRepository;
            private readonly IUnitofWork _unitofWork;
            public UpdatePartnerBankRatesCommandHandler(IUnitofWork unitofWork, IMapper mapper)
            {
                _mapper = mapper;
                _unitofWork = unitofWork;
                _NcmRepository = _unitofWork.NcmRepositoryAysnc;
            }

            public async Task<Response<int>> Handle(UpdatePartnerBankRatesCommand command, CancellationToken cancellationToken)
            {
                _NcmRepository.updatePartnerBankRates(command.CustomerBranchMasterlst);
                await _unitofWork.Complete();
                return new Response<int>(1);
            }

        }

    }

}




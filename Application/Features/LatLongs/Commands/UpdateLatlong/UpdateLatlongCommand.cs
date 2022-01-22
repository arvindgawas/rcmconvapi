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

namespace Application.Features.LatLongs.Commands.UpdateLatlong
{
    public class UpdateLatlongCommand : IRequest<Response<int>>
    {
        public List<latlonglistbool> lstlatlong { get; set; }
        public class UpdateLatlongCommandHandler : IRequestHandler<UpdateLatlongCommand, Response<int>>
        {

            private readonly IMapper _mapper;
            private readonly ILatlongRepository _LatLongRepository;
            private readonly IUnitofWork _unitofWork;
            public UpdateLatlongCommandHandler(IUnitofWork unitofWork, IMapper mapper)
            {
                _mapper = mapper;
                _unitofWork = unitofWork;
                _LatLongRepository = _unitofWork.LatLongRepositoryAysnc;
            }
            public async Task<Response<int>> Handle(UpdateLatlongCommand command, CancellationToken cancellationToken)
            {
                var objlatlong = _mapper.Map<latlonglist>(command);
                _LatLongRepository.updatelatlonglist(command.lstlatlong);
                await _unitofWork.Complete();
                return new Response<int>(1);
            }

        }
    }
}





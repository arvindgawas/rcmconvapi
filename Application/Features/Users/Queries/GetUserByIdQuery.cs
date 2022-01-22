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
namespace Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Response<UserMaster>>
    {
        public string LoginID { get; set; }
        public string UserPassword { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserMaster>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IMapper _mapper;
            public GetUserByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }
            public async Task<Response<UserMaster>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var userob = _mapper.Map<UserMaster>(query);

                var user = await _userRepository.GetByIdStrAsync(userob);

                //if (user == null) throw new ApiException($"User Not Found.");
                return new Response<UserMaster>(user);
            }
        }

    }
}

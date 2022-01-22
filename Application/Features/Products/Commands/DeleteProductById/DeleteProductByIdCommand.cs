using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<Response<int>>
    {
       
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Response<int>>
        {
            private readonly IUnitofWork _unitofWork;
            private readonly IProductRepositoryAsync _productRepository;
            //public DeleteProductByIdCommandHandler(IProductRepositoryAsync productRepository)
            public DeleteProductByIdCommandHandler(IUnitofWork unitofWork)
            {
                _unitofWork = unitofWork;
                _productRepository = _unitofWork.ProductRepositoryAsync;
            }
            public async Task<Response<int>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                _productRepository.DeleteAsync(product);
                await _unitofWork.Complete();
                return new Response<int>(product.Id);
            }
        }
    }
}

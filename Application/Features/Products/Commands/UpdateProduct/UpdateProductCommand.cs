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

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<int>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IUnitofWork _unitofWork;
            public UpdateProductCommandHandler(IUnitofWork unitofWork)
            {
                _unitofWork = unitofWork;
                _productRepository = _unitofWork.ProductRepositoryAsync;
            }
            public async Task<Response<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    throw new ApiException($"Product Not Found.");
                }
                else
                {
                    product.Name = command.Name;
                    product.Rate = command.Rate;
                    product.Description = command.Description;
                    _productRepository.UpdateAsync(product);
                    await _unitofWork.Complete();
                    return new Response<int>(product.Id);
                }
            }
        }
    }
}

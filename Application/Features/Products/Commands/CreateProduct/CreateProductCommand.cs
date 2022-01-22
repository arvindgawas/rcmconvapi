using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitofWork;
        //public CreateProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        public CreateProductCommandHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _productRepository = _unitofWork.ProductRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);
            await _unitofWork.Complete();
            return new Response<int>(product.Id);
        }
    }
}

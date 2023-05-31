using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Products;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts
{
    public class PaginationProductQueryHandler : IRequestHandler<PaginationProductQuery, PaginationVm<ProductVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<ProductVm>> Handle(PaginationProductQuery request, CancellationToken cancellationToken)
        {
            ProductSpecificationParams productSpecificationParams = new ProductSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                CategoryId = request.CategoryId,
                MaxPrice = request.MaxPrice,
                MinPrice = request.MinPrice,
                Rating = request.Rating,
                Status = request.Status,
            };

            ProductSpecification spec = new ProductSpecification(productSpecificationParams);
            IReadOnlyList<Product> products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);

            ProductForCountingSpecification specCount = new ProductForCountingSpecification(productSpecificationParams);
            int totalProducts = await _unitOfWork.Repository<Product>().CountAsync(spec);

            Decimal rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.PageSize));
            int totalPages = Convert.ToInt32(rounded);

            IReadOnlyList<ProductVm> data = _mapper.Map<IReadOnlyList<ProductVm>>(products);
            int productByPage = products.Count();

            return new PaginationVm<ProductVm>
            {
                Count = totalProducts,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productByPage
            };
        }
    }
}
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts
{
    public class PaginationProductQuery : PaginationBaseQuery, IRequest<PaginationVm<ProductVm>>
    {
        public int? CategoryId { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public int? Rating { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
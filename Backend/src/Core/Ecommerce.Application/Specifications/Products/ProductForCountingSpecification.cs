using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Products
{
    public class ProductForCountingSpecification : BaseSpecification<Product>
    {
        public ProductForCountingSpecification(ProductSpecificationParams productParams)
        : base(
            x => 
            (string.IsNullOrEmpty(productParams.Search) 
            || x.ProductName!.Contains(productParams.Search)
            || x.ProductDescription!.Contains(productParams.Search)) && 
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
            (!productParams.MinPrice.HasValue || x.ProductPrice >= productParams.MinPrice) &&
            (!productParams.MaxPrice.HasValue || x.ProductPrice <= productParams.MaxPrice) &&
            (!productParams.Status.HasValue || x.ProductStatus == productParams.Status) 
        )
        {
            
        }
        
    }
}
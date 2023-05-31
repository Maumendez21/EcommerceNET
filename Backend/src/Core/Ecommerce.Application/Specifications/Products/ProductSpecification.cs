using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Products
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationParams productParams)
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
            AddInclude(p => p.Reviews!);
            AddInclude(p => p.Images!);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.ProductName!);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.ProductName!);
                        break;
                    case "priceAsc":
                        AddOrderBy(p => p.ProductPrice!);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.ProductPrice!);
                        break;
                    case "ratingAsc":
                        AddOrderBy(p => p.ProductRating!);
                        break;
                    case "ratingDesc":
                        AddOrderByDescending(p => p.ProductRating!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }
            else {
                AddOrderByDescending(p => p.CreatedDate!);
            }
        }
    }
}
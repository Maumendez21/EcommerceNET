using Ecommerce.Application.Features.Images.Queries.Vms;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Models.Product;
using Ecommerce.Domain;

namespace Ecommerce.Application.Features.Products.Queries.Vms
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductRating { get; set; }
        public string? ProductSeller { get; set; }
        public int ProductStock { get; set; }
        public ICollection<ReviewVm>? Reviews { get; set; }
        public ICollection<ImageVm>? Images { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int ReviewsCount { get; set; }
        public ProductStatus ProductStatus { get; set; }
        
        public string? StatusLabel { 
            get{
                switch(ProductStatus)
                {
                    case ProductStatus.Active:{
                        return ProductStatusLabel.ACTIVE;
                    }
                    case ProductStatus.Inactive:{
                        return ProductStatusLabel.INACTIVE;
                    }
                    default: return ProductStatusLabel.INACTIVE;
                }
            }
            set {}
         }

    }
}
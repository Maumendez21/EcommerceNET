using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain
{
    public class Product : BaseDomainModel
    {
        public string? ProductName { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductRating { get; set; }
        public string? ProductSeller { get; set; }
        public int ProductStock { get; set; }
        public ProductStatus ProductStatus { get; set; } = ProductStatus.Active;
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Image>? Images { get; set; }
    }
}
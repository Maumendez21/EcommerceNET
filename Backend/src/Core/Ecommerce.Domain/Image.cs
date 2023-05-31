using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain
{
    public class Image : BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(4000)")]
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
        public string? ImagePublicCode { get; set; }
        public Product? Product { get; set; }
    }
}

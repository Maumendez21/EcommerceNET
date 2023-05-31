using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain
{
    public class Review :  BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string? ReviewName { get; set; }
        public int ReviewRating { get; set; }
        [Column(TypeName = "NVARCHAR(4000)")]
        public string? ReviewComentary { get; set; }
        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}

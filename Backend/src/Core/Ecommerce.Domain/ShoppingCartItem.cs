using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain
{
    public class ShoppingCartItem : BaseDomainModel
    {
        public string? CartProduct { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal CartPrice { get; set; }
        public int CartLot { get; set; }
        public int CartStock { get; set; }
        public string? CartImage { get; set; }
        public string? CartCategory { get; set; }
        public Guid? ShoppingCartMasterId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProdcutId { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }
    }
}

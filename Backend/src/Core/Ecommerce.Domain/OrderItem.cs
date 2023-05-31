using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class OrderItem :  BaseDomainModel
    {
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public decimal OrderItemPrice { get; set; }
        public int OrderItemLot { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
        public int ProductItemId { get; set; }
        public string? OrderItemName { get; set; }
        public string? OrderItemImg { get; set; }
        
    }
}
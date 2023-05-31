using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Order : BaseDomainModel
    {

        public Order()
        {
            
        }

        public Order(
            string buyerName,
            string buyerEmail,
            OrderAddress orderAddress,
            decimal subtotal,
            decimal total,
            decimal tax,
            decimal shippingPrice
        )
        {
            OrderBuyerName = buyerName;
            OrderBuyerUserName = buyerEmail;
            OrderAddress = orderAddress;
            OrderSubtotal = subtotal;
            OrderTotal = total;
            OrderTax = tax;
            OrderShippingPrice = shippingPrice;
        }

        public string? OrderBuyerName { get; set; }
        public string? OrderBuyerUserName { get; set; }
        public OrderAddress? OrderAddress { get; set; }
        public IReadOnlyList<OrderItem>? OrderItems { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal OrderSubtotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal OrderTotal { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal OrderTax { get; set; }
        
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal OrderShippingPrice { get; set; }
        public string? OrderPaymentIntentId { get; set; }
        public string? OrderClientSecret { get; set; }
        public string? OrderStripeApiKey { get; set; }
    }
}
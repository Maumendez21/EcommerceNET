using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class OrderAddress : BaseDomainModel
    {
        public string? OrderAddresse { get; set; }
        public string? OrderAddressCity { get; set; }
        public string? OrderAddressCountry { get; set; }
        public string? OrderAddressDepartament { get; set; }
        public string? OrderAddressPostalCode { get; set; }
        public string? OrderAddressUserName { get; set; }
        
    }
}
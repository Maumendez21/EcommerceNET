

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Address : BaseDomainModel
    {
        public string? Addresse { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressCountry { get; set; }
        public string? AddressDepartament { get; set; }
        public string? AddressPostalCode { get; set; }
        public string? AddressUserName { get; set; }
    }
}

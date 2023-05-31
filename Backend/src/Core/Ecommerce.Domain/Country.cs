using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Country : BaseDomainModel
    {
        public string? CountryName { get; set; }
        public string? CountryIso2 { get; set; }
        public string? CountryIso3 { get; set; }
    }
}

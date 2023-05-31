using System.Runtime.Serialization;

namespace Ecommerce.Domain
{
    public enum ProductStatus
    {
        [EnumMember(Value = "Product Inactive")]
        Inactive,
        [EnumMember(Value = "Product Active")]
        Active
    }
}
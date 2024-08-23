namespace Ecommerce.Application.Features.Auths.Countries.Vms
{
    public class CountryVm
    {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? CountryIso2 { get; set; }
        public string? CountryIso3 { get; set; }
    }
}
namespace Ecommerce.Application.Features.Images.Queries.Vms
{
    public class ImageVm
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
        public string? ImagePublicCode { get; set; }
    }
}
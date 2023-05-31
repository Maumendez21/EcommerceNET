namespace Ecommerce.Application.Features.Reviews.Queries.Vms
{
    public class ReviewVm
    {
        public int Id { get; set; }
        public string? ReviewName { get; set; }
        public int ReviewRating { get; set; }
        public string? ReviewComentary { get; set; }
        public int ProductId { get; set; }
    }
}
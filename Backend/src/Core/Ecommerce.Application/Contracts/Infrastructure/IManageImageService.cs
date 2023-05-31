using Ecommerce.Application.Models.ImageMangment;

namespace Ecommerce.Application.Contracts.Infrastructure;

public interface IManageImageService
{
    Task<ImageResponse> UploadImage(ImageData imageStream);
       
}

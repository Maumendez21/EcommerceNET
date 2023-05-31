using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infraestructure.Persistence
{
    public class EcommerceDbContextData
    {
        public static async Task LoadDataAsync(
            EcommerceDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
        )
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.USER));
                }

                if (!userManager.Users.Any())
                {
                    var userAdmin = new User
                    {
                        Name = "Mau",
                        LastName = "Mendez",
                        Email = "maumendezm21@gmail.com",
                        UserName = "Kingmau",
                        AvatarUrl = "https://images-eds-ssl.xboxlive.com/image?url=wHwbXKif8cus8csoZ03RW_ES.ojiJijNBGRVUbTnZKvlHqqbdJfZWiFtiClZ7rrQs4CkUwWbUzzV.Cu_Fz6Rf.r5065ZnvlgmUR8OE9_PHRGLxZIvks617KLCML3LQNsXbc0NaVLkxu3Ym7vAvmGQrDsC7S3rftbWNdI.Zuue4lGz.FPpFVPNDpKLD08Q3Sm&format=png",
                    };

                    await userManager.CreateAsync(userAdmin, "Admin123*");
                    await userManager.AddToRoleAsync(userAdmin, Role.ADMIN);

                    var userUser = new User
                    {
                        Name = "Luis",
                        LastName = "Miguel",
                        Email = "luism@gmail.com",
                        UserName = "lmxlm",
                        AvatarUrl = "https://i.pinimg.com/736x/18/7c/f5/187cf5793ac7ede32b6ee16a15ea3cac.jpg",
                    };

                    await userManager.CreateAsync(userUser, "User123*");
                    await userManager.AddToRoleAsync(userUser, Role.USER);
                }
            
                if (!context.Categories!.Any())
                {
                    var categoryData = File.ReadAllText("../Infraestructure/Data/category.json");
                    var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                    await context.Categories!.AddRangeAsync(categories!);
                    await context.SaveChangesAsync();
                }
                if (!context.Categories!.Any())
                {
                    var countryData = File.ReadAllText("../Infraestructure/Data/countries.json");
                    var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                    await context.Countries!.AddRangeAsync(countries!);
                    await context.SaveChangesAsync();
                }
                if (!context.Products!.Any())
                {
                    var productData = File.ReadAllText("../Infraestructure/Data/product.json");
                    var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                    await context.Products!.AddRangeAsync(products!);
                    await context.SaveChangesAsync();
                }
                if (!context.Images!.Any())
                {
                    var imageData = File.ReadAllText("../Infraestructure/Data/image.json");
                    var images = JsonConvert.DeserializeObject<List<Image>>(imageData);
                    await context.Images!.AddRangeAsync(images!);
                    await context.SaveChangesAsync();
                }
                if (!context.Reviews!.Any())
                {
                    var reviewData = File.ReadAllText("../Infraestructure/Data/review.json");
                    var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewData);
                    await context.Reviews!.AddRangeAsync(reviews!);
                    await context.SaveChangesAsync();
                }
            
            
            }
            catch (System.Exception e)
            {
                var logger  = loggerFactory.CreateLogger<EcommerceDbContextData>();
                logger.LogError(e.Message);
            }
        }
    }
}
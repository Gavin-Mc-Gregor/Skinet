using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!storeContext.ProductBrand.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        storeContext.ProductBrand.Add(brand);
                    }
                    await storeContext.SaveChangesAsync();

                }

                if (!storeContext.ProductType.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                    {
                        storeContext.ProductType.Add(type);
                    }
                    await storeContext.SaveChangesAsync();

                }

                if (!storeContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        storeContext.Products.Add(product);
                    }
                    await storeContext.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContext>();
                logger.LogError(ex, "Error occurred while seeding data");
            }
        }
    }
}
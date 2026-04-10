using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data;
public static class StoredDbContextSeed
{
    public static async Task SeedAsync(StoreDbContext _context)
    {
        if (_context.Products.Count() == 0) {
            //brands
            //read data file json
            var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeeds/brands.json");
            //convert from json to list of objects
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            if (brands?.Count() > 0)
            {
                foreach (var item in brands)
                {
                    _context.Set<ProductBrand>().Add(item);
                }
                await _context.SaveChangesAsync();
            }
            //Categories
            //read data file json
            var categoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeds/categories.json");
            //convert from json to list of objects
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
            if (categories?.Count() > 0)
            {
                foreach (var item in categories)
                {
                    _context.Set<ProductCategory>().Add(item);
                }
                await _context.SaveChangesAsync();
            }
            //Products
            //read data file json
            var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeds/products.json");
            //convert from json to list of objects
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products?.Count() > 0)
            {
                foreach (var item in products)
                {
                    _context.Set<Product>().Add(item);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Talabat.APIs.Helpers;
using Talabat.Core.Repository.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((servicesProvider) =>
            {
                var cofigurations = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(cofigurations);  
            }
            );


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

             // builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));
             // builder.Services.AddScoped<ProductPictureUrlResolver>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _context = services.GetRequiredService<StoreDbContext>();
            try
            {
               await _context.Database.MigrateAsync();
               await StoredDbContextSeed.SeedAsync(_context);
            }
            catch (Exception ex)    
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred Applying Migratoins");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}

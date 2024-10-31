using Application.ProductManagement;
using Application.UserMangement;
using Domain;
using Domain.Repositories;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add SQLite DbContext
            services.AddDbContext<BitcubeDevTaskDbContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BitcubeDevTaskDbContext>()
                .AddDefaultTokenProviders();

            // Add CORS policy
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithOrigins("https://your-allowed-origin.com") // Specify allowed origins here
                       .AllowCredentials();
            }));

            // Add your repository interfaces and implementations for Dependency Injection
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IProductManagementService, ProductManagementService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add services to the container
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Optional: For detailed error pages in Development
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BitcubeDeveloperTask API V1"); // Update with your endpoint
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Handle exceptions gracefully in production
                app.UseHsts(); // HTTP Strict Transport Security
            }

            app.UseHttpsRedirection();
            app.UseRouting(); // Add routing middleware

            app.UseAuthorization();

            // Map controllers to endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // This line maps attribute-routed controllers to the app's request pipeline
            });
        }
    }
}
